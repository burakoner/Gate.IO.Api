namespace Gate.IO.Api.Otc;

/// <summary>
/// Gate.IO OTC REST API Client
/// </summary>
public class GateOtcRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string otc = "otc";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateOtcRestApiClient(GateRestApiClient root) => _ = root;

    private static string FormatTime(DateTime? time)
        => time?.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

    private static void Require(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{parameterName} is required", parameterName);
    }

    private async Task<RestCallResult<T>> SendOtcDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await _.SendRequestInternal<GateOtcResponse<T>>(_.GetUrl(api, v4, otc, endpoint), method, ct, true, queryParameters, bodyParameters).ConfigureAwait(false);
        return result.Success ? result.As(result.Data?.Data) : result.As<T>(default);
    }

    private static RestCallResult<T> AsSensitiveOtcFailure<T>(RestCallResult<JToken> result, JsonSerializer serializer, string diagnostic) where T : class
    {
        if (result.Error is DeserializeError)
            return result.AsError<T>(new DeserializeError(diagnostic, null));
        if (result.Error is ServerError && !string.IsNullOrEmpty(result.Raw))
        {
            using var reader = new JsonTextReader(new System.IO.StringReader(result.Raw)) { DateParseHandling = DateParseHandling.None };
            if (serializer.Deserialize<JToken>(reader) is JObject envelope && envelope["code"]?.Type == JTokenType.Integer
                && int.TryParse(envelope["code"].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var code) && code != 0)
                return result.AsError<T>(new ServerError(code,
                    envelope["message"]?.Type == JTokenType.String ? (string)envelope["message"] : "OTC request rejected", result.Error.Data));
        }
        return result.As<T>(null);
    }

    private async Task<RestCallResult<GateOtcActionResult>> SendOtcActionRequestAsync(
        string endpoint, ParameterCollection body, CancellationToken ct, bool legacyFiatSeconds = false)
    {
        var serializer = JsonSerializer.Create(new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
        serializer.Converters.Add(new GateOtcSensitiveEnvelopeConverter());
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, otc, endpoint), HttpMethod.Post, ct,
            true, bodyParameters: body, deserializer: serializer).ConfigureAwait(false);
        if (!result.Success)
            return AsSensitiveOtcFailure<GateOtcActionResult>(result, serializer, "Invalid OTC action acknowledgement");
        var token = result.Data;
        if (!string.IsNullOrEmpty(result.Raw))
        {
            using var reader = new JsonTextReader(new System.IO.StringReader(result.Raw)) { DateParseHandling = DateParseHandling.None };
            token = serializer.Deserialize<JToken>(reader);
        }
        if (token is not JObject envelope || envelope["code"]?.Type != JTokenType.Integer
            || !int.TryParse(envelope["code"].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var code))
            return result.AsError<GateOtcActionResult>(new DeserializeError("OTC action acknowledgement requires an integer business code", null));
        var message = envelope["message"]?.Type == JTokenType.String ? (string)envelope["message"] : null;
        if (code != 0)
            return result.AsError<GateOtcActionResult>(new ServerError(code, message ?? "OTC action rejected"));
        if (message == null || envelope["timestamp"]?.Type != JTokenType.Integer
            || !long.TryParse(envelope["timestamp"].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var timestamp))
            return result.AsError<GateOtcActionResult>(new DeserializeError("OTC action acknowledgement requires a message and integer timestamp", null));
        try
        {
            // The user explicitly retained DateTime. Preserve each existing interpretation, not a new wire-unit rule.
            return result.As(legacyFiatSeconds
                ? new GateOtcActionResult { Code = code, Message = message, Timestamp = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime }
                : envelope.ToObject<GateOtcActionResult>(serializer));
        }
        catch (JsonException) { return result.AsError<GateOtcActionResult>(new DeserializeError("Invalid OTC action acknowledgement", null)); }
        catch (ArgumentOutOfRangeException) { return result.AsError<GateOtcActionResult>(new DeserializeError("OTC action timestamp is outside the legacy date range", null)); }
    }

    private static void AddSupplementFile(List<KeyValuePair<string, GateOtcFileUpload>> files, string field,
        string base64, GateOtcFileUpload upload, string parameterName)
    {
        if (base64 != null && upload != null)
            throw new ArgumentException("File representations are mutually exclusive", parameterName);
        if (base64 != null)
        {
            Require(base64, parameterName);
            try { upload = new GateOtcFileUpload { Content = Convert.FromBase64String(base64), FileName = field }; }
            catch (FormatException) { throw new ArgumentException("Supplement file content must be Base64", parameterName); }
        }
        if (upload != null) files.Add(new KeyValuePair<string, GateOtcFileUpload>(field, upload));
    }

    /// <summary>
    /// Issue temporary S3 POST credentials. Does not upload a file, submit materials or retry automatically.
    /// https://www.gate.com/docs/developers/apiv4/en/otc/#pre-upload-file-temporary-bucket
    /// </summary>
    /// <param name="contentType">Supported MIME type; the client sends its documented base64 value.</param>
    /// <param name="scene">Optional scene. Omission leaves the server default general unchanged.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Sensitive Policy fields and the complete acknowledgement; not upload confirmation.</returns>
    public Task<RestCallResult<GateOtcUploadPreUploadResponse>> CreatePreUploadAsync(
        GateOtcUploadContentType contentType, GateOtcUploadScene? scene = null, CancellationToken ct = default)
        => CreatePreUploadAsync(new GateOtcUploadPreUploadRequest { ContentType = contentType, Scene = scene }, ct);

    /// <summary>
    /// Issue temporary S3 POST credentials only. Preserve all returned fields unchanged during a separate
    /// direct upload; Gate credentials must not be forwarded to the returned URL. No file content is accepted here.
    /// </summary>
    /// <param name="request">Pre-upload MIME and optional scene.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The complete current pre-upload acknowledgement, with nonzero business codes reported as errors.</returns>
    public async Task<RestCallResult<GateOtcUploadPreUploadResponse>> CreatePreUploadAsync(
        GateOtcUploadPreUploadRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (!Enum.IsDefined(typeof(GateOtcUploadContentType), request.ContentType))
            throw new ArgumentOutOfRangeException(nameof(request.ContentType));
        if (request.Scene.HasValue && !Enum.IsDefined(typeof(GateOtcUploadScene), request.Scene.Value))
            throw new ArgumentOutOfRangeException(nameof(request.Scene));

        var body = new ParameterCollection();
        body.AddEnum("content_type", request.ContentType);
        body.AddOptionalEnum("scene", request.Scene);
        // Opaque signed S3 strings must not be normalized into dates by the JSON reader.
        var serializer = JsonSerializer.Create(new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
        serializer.Converters.Add(new GateOtcSensitiveEnvelopeConverter());
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, otc, "upload/pre_upload"),
            HttpMethod.Post, ct, true, bodyParameters: body, deserializer: serializer).ConfigureAwait(false);
        if (!result.Success)
            return AsSensitiveOtcFailure<GateOtcUploadPreUploadResponse>(result, serializer, "Invalid OTC pre-upload acknowledgement");

        var token = result.Data;
        if (!string.IsNullOrEmpty(result.Raw))
        {
            // RawResponse's dependency path parses dates before invoking the supplied serializer.
            using var reader = new JsonTextReader(new System.IO.StringReader(result.Raw)) { DateParseHandling = DateParseHandling.None };
            token = serializer.Deserialize<JToken>(reader);
        }

        if (token is not JObject envelope || envelope["code"]?.Type != JTokenType.Integer
            || !int.TryParse(envelope["code"].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var code))
            return result.AsError<GateOtcUploadPreUploadResponse>(new DeserializeError("Pre-upload acknowledgement requires an integer business code", null));
        if (code != 0)
            return result.AsError<GateOtcUploadPreUploadResponse>(new ServerError(code,
                envelope["message"]?.Type == JTokenType.String ? (string)envelope["message"] : "OTC pre-upload rejected"));
        try
        {
            var response = envelope.ToObject<GateOtcUploadPreUploadResponse>(serializer);
            return result.As(response);
        }
        catch (JsonException)
        {
            // Do not copy Policy/credential/file-key payloads into an error message or Error.Data.
            return result.AsError<GateOtcUploadPreUploadResponse>(new DeserializeError("Invalid or incomplete OTC pre-upload acknowledgement", null));
        }
    }

    /// <summary>
    /// Fiat and stablecoin quote
    /// </summary>
    /// <param name="side">Quote direction</param>
    /// <param name="payCoin">Currency the user pays</param>
    /// <param name="getCoin">Currency the user receives</param>
    /// <param name="payAmount">User payment currency amount</param>
    /// <param name="getAmount">Amount of currency received by the user</param>
    /// <param name="createQuoteToken">Generate quote token for order placement</param>
    /// <param name="promotionCode">Promotion code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcQuote>> GetQuoteAsync(
        GateOtcQuoteSide side,
        string payCoin,
        string getCoin,
        decimal? payAmount = null,
        decimal? getAmount = null,
        bool createQuoteToken = false,
        string promotionCode = null,
        CancellationToken ct = default)
        => GetQuoteAsync(new GateOtcQuoteRequest
        {
            Side = side,
            PayCoin = payCoin,
            GetCoin = getCoin,
            PayAmount = payAmount,
            GetAmount = getAmount,
            CreateQuoteToken = createQuoteToken,
            PromotionCode = promotionCode,
        }, ct);

    /// <summary>
    /// Fiat and stablecoin quote
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOtcQuote>> GetQuoteAsync(GateOtcQuoteRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.PayCoin, nameof(request.PayCoin));
        Require(request.GetCoin, nameof(request.GetCoin));
        if (!Enum.IsDefined(typeof(GateOtcQuoteSide), request.Side))
            throw new ArgumentOutOfRangeException(nameof(request.Side));
        if (request.Side == GateOtcQuoteSide.Pay && !request.PayAmount.HasValue)
            throw new ArgumentException("PayAmount is required for PAY quotes", nameof(request.PayAmount));
        if (request.Side == GateOtcQuoteSide.Get && !request.GetAmount.HasValue)
            throw new ArgumentException("GetAmount is required for GET quotes", nameof(request.GetAmount));

        var parameters = new ParameterCollection
        {
            { "pay_coin", request.PayCoin },
            { "get_coin", request.GetCoin },
            { "create_quote_token", request.CreateQuoteToken ? "1" : "0" },
        };
        parameters.AddEnum("side", request.Side);
        parameters.AddOptionalString("pay_amount", request.PayAmount);
        parameters.AddOptionalString("get_amount", request.GetAmount);
        parameters.AddOptional("promotion_code", request.PromotionCode);

        var result = await _.SendRequestInternal<GateOtcResponse<GateOtcQuote>>(
            _.GetUrl(api, v4, otc, "quote"),
            HttpMethod.Post,
            ct,
            true,
            bodyParameters: parameters).ConfigureAwait(false);

        var quote = result.Data?.Data;
        if (quote != null)
            quote.Timestamp = result.Data.Timestamp ?? default;

        return result.Success ? result.As(quote) : result.As<GateOtcQuote>(default);
    }

    /// <summary>
    /// Create fiat order using the legacy FIAT validation side. Use the DTO overload for the quote's
    /// explicit PAY/GET side and optional remittance name. The result is not proof of bank settlement.
    /// </summary>
    /// <param name="type">BUY for on-ramp or SELL for off-ramp</param>
    /// <param name="cryptoCurrency">Cryptocurrency</param>
    /// <param name="fiatCurrency">Fiat currency</param>
    /// <param name="cryptoAmount">Amount of cryptocurrency</param>
    /// <param name="fiatAmount">Fiat amount</param>
    /// <param name="quoteToken">Quote token returned by the quote API</param>
    /// <param name="bankId">Bank card ID used for the order</param>
    /// <param name="promotionCode">Promotion code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CreateFiatOrderAsync(
        GateOtcOrderType type,
        string cryptoCurrency,
        string fiatCurrency,
        decimal cryptoAmount,
        decimal fiatAmount,
        string quoteToken,
        long bankId,
        string promotionCode = null,
        CancellationToken ct = default)
        => CreateFiatOrderAsync(new GateOtcFiatOrderRequest
        {
            Type = type,
            CryptoCurrency = cryptoCurrency,
            FiatCurrency = fiatCurrency,
            CryptoAmount = cryptoAmount,
            FiatAmount = fiatAmount,
            QuoteToken = quoteToken,
            BankId = bankId,
            PromotionCode = promotionCode,
        }, ct);

    /// <summary>
    /// Create fiat order with an explicit quote-validation side and optional remittance name.
    /// https://www.gate.com/docs/developers/apiv4/en/otc/#create-fiat-order
    /// An acknowledgement has no order ID and does not confirm payment or settlement.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOtcActionResult>> CreateFiatOrderAsync(GateOtcFiatOrderRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        Require(request.CryptoCurrency, nameof(request.CryptoCurrency));
        Require(request.FiatCurrency, nameof(request.FiatCurrency));
        Require(request.QuoteToken, nameof(request.QuoteToken));
        if (!Enum.IsDefined(typeof(GateOtcOrderType), request.Type))
            throw new ArgumentOutOfRangeException(nameof(request.Type));
        if (!Enum.IsDefined(typeof(GateOtcOrderKind), request.Side) || request.Side == GateOtcOrderKind.Stable)
            throw new ArgumentException("Fiat order side must be FIAT, CRYPTO, PAY or GET", nameof(request.Side));
        if (request.ReceiveType.HasValue && !Enum.IsDefined(typeof(GateOtcReceiveType), request.ReceiveType.Value))
            throw new ArgumentOutOfRangeException(nameof(request.ReceiveType));
        // Explicit actual bank identity is a client safety constraint, not an inferred default bank.
        if (request.BankId <= 0) throw new ArgumentOutOfRangeException(nameof(request.BankId));

        var parameters = new ParameterCollection
        {
            { "crypto_currency", request.CryptoCurrency },
            { "fiat_currency", request.FiatCurrency },
            { "quote_token", request.QuoteToken },
            { "bank_id", request.BankId.ToString(CultureInfo.InvariantCulture) },
        };
        parameters.AddEnum("type", request.Type);
        parameters.AddEnum("side", request.Side);
        parameters.AddString("crypto_amount", request.CryptoAmount);
        parameters.AddString("fiat_amount", request.FiatAmount);
        parameters.AddOptional("promotion_code", request.PromotionCode);
        parameters.AddOptionalEnum("receive_type", request.ReceiveType);

        return await SendOtcActionRequestAsync("order/create", parameters, ct, legacyFiatSeconds: true).ConfigureAwait(false);
    }

    /// <summary>
    /// Create stablecoin order
    /// </summary>
    /// <param name="payCoin">Currency paid by the user</param>
    /// <param name="getCoin">Currency to be received by the user</param>
    /// <param name="payAmount">User payment currency amount</param>
    /// <param name="getAmount">Amount of currency received by the user</param>
    /// <param name="side">Quote direction returned by the quote API</param>
    /// <param name="quoteToken">Quote token returned by the quote API</param>
    /// <param name="promotionCode">Promotion code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CreateStableCoinOrderAsync(
        string payCoin,
        string getCoin,
        decimal payAmount,
        decimal getAmount,
        GateOtcQuoteSide side,
        string quoteToken,
        string promotionCode = null,
        CancellationToken ct = default)
        => CreateStableCoinOrderAsync(new GateOtcStableCoinOrderRequest
        {
            PayCoin = payCoin,
            GetCoin = getCoin,
            PayAmount = payAmount,
            GetAmount = getAmount,
            Side = side,
            QuoteToken = quoteToken,
            PromotionCode = promotionCode,
        }, ct);

    /// <summary>
    /// Create stablecoin order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CreateStableCoinOrderAsync(GateOtcStableCoinOrderRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.PayCoin, nameof(request.PayCoin));
        Require(request.GetCoin, nameof(request.GetCoin));
        Require(request.QuoteToken, nameof(request.QuoteToken));
        if (!Enum.IsDefined(typeof(GateOtcQuoteSide), request.Side))
            throw new ArgumentOutOfRangeException(nameof(request.Side));

        var parameters = new ParameterCollection
        {
            { "pay_coin", request.PayCoin },
            { "get_coin", request.GetCoin },
            { "quote_token", request.QuoteToken },
        };
        parameters.AddString("pay_amount", request.PayAmount);
        parameters.AddString("get_amount", request.GetAmount);
        parameters.AddEnum("side", request.Side);
        parameters.AddOptional("promotion_code", request.PromotionCode);

        return _.SendRequestInternal<GateOtcActionResult>(_.GetUrl(api, v4, otc, "stable_coin/order/create"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get user bank card list
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOtcBankAccount>>> GetBankAccountsAsync(CancellationToken ct = default)
    {
        var result = await SendOtcDataRequestAsync<GateOtcBankList>("bank/list", HttpMethod.Get, ct).ConfigureAwait(false);
        return result.Success ? result.As(result.Data?.Lists ?? []) : result.As<List<GateOtcBankAccount>>(default);
    }

    /// <summary>
    /// Submit bank card materials using exactly one direct-file or pre-upload key source. Not review approval.
    /// https://www.gate.com/docs/developers/apiv4/en/otc/#create-bank-card
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOtcBankCreateResult>> CreateBankCardAsync(GateOtcBankCreateRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.BankAccountName, nameof(request.BankAccountName));
        Require(request.BankName, nameof(request.BankName));
        Require(request.BankCountry, nameof(request.BankCountry));
        Require(request.BankAddress, nameof(request.BankAddress));
        Require(request.Iban, nameof(request.Iban));
        Require(request.Swift, nameof(request.Swift));
        var sourceCount = (request.DocumentationFile != null ? 1 : 0) + (request.DocumentationUpload != null ? 1 : 0)
            + (request.DocumentationFileKey != null ? 1 : 0);
        if (sourceCount == 0) throw new ArgumentException("Exactly one proof source is required", nameof(request.DocumentationFile));
        if (sourceCount != 1) throw new ArgumentException("Proof sources are mutually exclusive", nameof(request));
        var upload = request.DocumentationUpload;
        if (request.DocumentationFile != null)
        {
            Require(request.DocumentationFile, nameof(request.DocumentationFile));
            try { upload = new GateOtcFileUpload { Content = Convert.FromBase64String(request.DocumentationFile), FileName = "documentation_file" }; }
            catch (FormatException) { throw new ArgumentException("DocumentationFile must be Base64 file content", nameof(request.DocumentationFile)); }
        }
        if (request.DocumentationFileKey != null)
        {
            Require(request.DocumentationFileKey, nameof(request.DocumentationFileKey));
            Require(request.FileType, nameof(request.FileType));
        }

        var form = new ParameterCollection
        {
            { "bank_account_name", request.BankAccountName },
            { "bank_name", request.BankName },
            { "bank_country", request.BankCountry },
            { "bank_address", request.BankAddress },
            { "iban", request.Iban },
            { "swift", request.Swift },
        };
        form.AddOptional("remittance_line_number", request.RemittanceLineNumber);
        form.AddOptional("agent_bank_name", request.AgentBankName);
        form.AddOptional("agent_bank_swift", request.AgentBankSwift);
        form.AddOptional("documentation_file_key", request.DocumentationFileKey);
        form.AddOptional("file_type", request.FileType);

        var body = upload == null ? GateMultipartFormData.CreateBodyParameters(form)
            : GateMultipartFormData.CreateBodyParameters(form, "documentation_file", upload);
        var serializer = JsonSerializer.Create(new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
        serializer.Converters.Add(new GateOtcSensitiveEnvelopeConverter());
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, otc, "bank/create"), HttpMethod.Post, ct,
            true, bodyParameters: body, deserializer: serializer).ConfigureAwait(false);
        if (!result.Success)
            return AsSensitiveOtcFailure<GateOtcBankCreateResult>(result, serializer, "Invalid bank-create acknowledgement");
        var token = result.Data;
        if (!string.IsNullOrEmpty(result.Raw))
        {
            using var reader = new JsonTextReader(new System.IO.StringReader(result.Raw)) { DateParseHandling = DateParseHandling.None };
            token = serializer.Deserialize<JToken>(reader);
        }
        if (token is not JObject envelope || envelope["code"]?.Type != JTokenType.Integer
            || !int.TryParse(envelope["code"].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var code))
            return result.AsError<GateOtcBankCreateResult>(new DeserializeError("Bank-create acknowledgement requires an integer business code", null));
        if (code != 0)
            return result.AsError<GateOtcBankCreateResult>(new ServerError(code,
                envelope["message"]?.Type == JTokenType.String ? (string)envelope["message"] : "OTC bank submission rejected"));
        try
        {
            var response = envelope.ToObject<GateOtcBankCreateResponse>(serializer);
            response.Data.Code = response.Code;
            response.Data.Message = response.Message;
            response.Data.Timestamp = response.Timestamp;
            return result.As(response.Data);
        }
        catch (JsonException)
        {
            return result.AsError<GateOtcBankCreateResult>(new DeserializeError("Invalid or incomplete bank-create acknowledgement", null));
        }
    }

    /// <summary>
    /// Delete bank card
    /// </summary>
    /// <param name="bankId">Bank card ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> DeleteBankCardAsync(string bankId, CancellationToken ct = default)
        => DeleteBankCardAsync(new GateOtcBankIdRequest { BankId = bankId }, ct);

    /// <summary>
    /// Delete bank card
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> DeleteBankCardAsync(GateOtcBankIdRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        Require(request.BankId, nameof(request.BankId));

        var parameters = new ParameterCollection
        {
            { "bank_id", request.BankId },
        };

        return _.SendRequestInternal<GateOtcActionResult>(_.GetUrl(api, v4, otc, "bank/delete"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set default bank card
    /// </summary>
    /// <param name="bankId">Bank card ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> SetDefaultBankCardAsync(string bankId, CancellationToken ct = default)
        => SetDefaultBankCardAsync(new GateOtcBankIdRequest { BankId = bankId }, ct);

    /// <summary>
    /// Set default bank card
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> SetDefaultBankCardAsync(GateOtcBankIdRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        Require(request.BankId, nameof(request.BankId));

        var parameters = new ParameterCollection
        {
            { "bank_id", request.BankId },
        };

        return _.SendRequestInternal<GateOtcActionResult>(_.GetUrl(api, v4, otc, "bank/set_default"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get the bank card supplement checklist
    /// </summary>
    /// <param name="bankId">Bank card ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcBankSupplementChecklist>> GetBankSupplementChecklistAsync(string bankId, CancellationToken ct = default)
    {
        Require(bankId, nameof(bankId));

        var parameters = new ParameterCollection
        {
            { "bank_id", bankId },
        };

        return SendOtcDataRequestAsync<GateOtcBankSupplementChecklist>("bank/bank_supplement_checklist", HttpMethod.Get, ct, parameters);
    }

    /// <summary>
    /// Submit personal bank materials selected from the matching checklist. Files and pre-upload JSON can be mixed.
    /// https://www.gate.com/docs/developers/apiv4/en/otc/#submit-bank-card-supplement-materials-personal
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> SubmitPersonalBankSupplementAsync(GateOtcBankPersonalSupplementRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.BankId, nameof(request.BankId));

        var form = new ParameterCollection
        {
            { "bank_id", request.BankId },
        };
        form.AddOptional("relationship_proof", request.RelationshipProof);
        var files = new List<KeyValuePair<string, GateOtcFileUpload>>();
        AddSupplementFile(files, "id_document_front", request.IdDocumentFront, request.IdDocumentFrontUpload, nameof(request.IdDocumentFront));
        AddSupplementFile(files, "id_document_back", request.IdDocumentBack, request.IdDocumentBackUpload, nameof(request.IdDocumentBack));
        AddSupplementFile(files, "address_proof", request.AddressProof, request.AddressProofUpload, nameof(request.AddressProof));
        return SendOtcActionRequestAsync("bank/personal/bank_supplement", GateMultipartFormData.CreateBodyParameters(form, files), ct);
    }

    /// <summary>
    /// Submit enterprise bank materials selected from the matching checklist. Files and pre-upload JSON can be mixed.
    /// https://www.gate.com/docs/developers/apiv4/en/otc/#submit-bank-card-supplement-materials-enterprise
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> SubmitEnterpriseBankSupplementAsync(GateOtcBankEnterpriseSupplementRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.BankId, nameof(request.BankId));

        var form = new ParameterCollection
        {
            { "bank_id", request.BankId },
        };
        form.AddOptional("uid", request.UserId);
        form.AddOptional("relationship_proof", request.RelationshipProof);
        var files = new List<KeyValuePair<string, GateOtcFileUpload>>();
        AddSupplementFile(files, "certificate", request.Certificate, request.CertificateUpload, nameof(request.Certificate));
        AddSupplementFile(files, "share_holders", request.ShareHolders, request.ShareHoldersUpload, nameof(request.ShareHolders));
        AddSupplementFile(files, "passport", request.Passport, request.PassportUpload, nameof(request.Passport));
        AddSupplementFile(files, "share_holding_structure", request.ShareHoldingStructure, request.ShareHoldingStructureUpload, nameof(request.ShareHoldingStructure));
        AddSupplementFile(files, "funds_statement", request.FundsStatement, request.FundsStatementUpload, nameof(request.FundsStatement));
        AddSupplementFile(files, "additional", request.Additional, request.AdditionalUpload, nameof(request.Additional));
        return SendOtcActionRequestAsync("bank/enterprise/bank_supplement", GateMultipartFormData.CreateBodyParameters(form, files), ct);
    }

    /// <summary>
    /// Notify payment on a fiat BUY order, using the unchanged receipt key. Not bank settlement confirmation.
    /// https://www.gate.com/docs/developers/apiv4/en/otc/#mark-fiat-order-as-paid-deposit-confirmation
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="paymentReceiptFileKey">Required payment receipt file key</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> MarkFiatOrderAsPaidAsync(string orderId, string paymentReceiptFileKey, CancellationToken ct = default)
        => MarkFiatOrderAsPaidAsync(new GateOtcMarkOrderPaidRequest { OrderId = orderId, PaymentReceiptFileKey = paymentReceiptFileKey }, ct);

    /// <summary>
    /// Mark fiat order as paid
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> MarkFiatOrderAsPaidAsync(GateOtcMarkOrderPaidRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.OrderId, nameof(request.OrderId));
        Require(request.PaymentReceiptFileKey, nameof(request.PaymentReceiptFileKey));

        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId },
            { "payment_receipt_file_key", request.PaymentReceiptFileKey },
        };
        parameters.AddOptional("client_order_id", request.ClientOrderId);
        parameters.AddOptional("payment_receipt", request.PaymentReceipt);

        return SendOtcActionRequestAsync("order/paid", parameters, ct);
    }

    /// <summary>
    /// Fiat order cancellation
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CancelFiatOrderAsync(long orderId, CancellationToken ct = default)
        => CancelFiatOrderAsync(orderId.ToString(CultureInfo.InvariantCulture), ct);

    /// <summary>
    /// Fiat order cancellation
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CancelFiatOrderAsync(string orderId, CancellationToken ct = default)
        => CancelFiatOrderAsync(new GateOtcOrderIdRequest { OrderId = orderId }, ct);

    /// <summary>
    /// Fiat order cancellation
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CancelFiatOrderAsync(GateOtcOrderIdRequest request, CancellationToken ct = default)
    {
        Require(request.OrderId, nameof(request.OrderId));

        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId },
        };

        return _.SendRequestInternal<GateOtcActionResult>(_.GetUrl(api, v4, otc, "order/cancel"), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Fiat order list
    /// </summary>
    /// <param name="type">BUY or SELL</param>
    /// <param name="fiatCurrency">Fiat currency</param>
    /// <param name="cryptoCurrency">Digital currency</param>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="status">Order status: DONE, CANCEL, PROCESSING, or DISBURSED</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcFiatOrderPage>> GetFiatOrdersAsync(
        GateOtcOrderType? type = null,
        string fiatCurrency = null,
        string cryptoCurrency = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string status = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
        => GetFiatOrdersAsync(new GateOtcFiatOrderListRequest
        {
            Type = type,
            FiatCurrency = fiatCurrency,
            CryptoCurrency = cryptoCurrency,
            StartTime = startTime,
            EndTime = endTime,
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize,
        }, ct);

    /// <summary>
    /// Fiat order list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcFiatOrderPage>> GetFiatOrdersAsync(GateOtcFiatOrderListRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("fiat_currency", request.FiatCurrency);
        parameters.AddOptional("crypto_currency", request.CryptoCurrency);
        parameters.AddOptional("start_time", FormatTime(request.StartTime));
        parameters.AddOptional("end_time", FormatTime(request.EndTime));
        parameters.AddOptional("status", request.Status);
        parameters.AddOptional("pn", request.PageNumber);
        parameters.AddOptional("ps", request.PageSize);

        return SendOtcDataRequestAsync<GateOtcFiatOrderPage>("order/list", HttpMethod.Get, ct, parameters);
    }

    /// <summary>
    /// Stablecoin order list
    /// </summary>
    /// <param name="pageSize">Number of records per page</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="coinName">Order currency</param>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="status">Status: PROCESSING, DONE, or FAILED</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcStableCoinOrderPage>> GetStableCoinOrdersAsync(
        int? pageSize = null,
        int? pageNumber = null,
        string coinName = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string status = null,
        CancellationToken ct = default)
        => GetStableCoinOrdersAsync(new GateOtcStableCoinOrderListRequest
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            CoinName = coinName,
            StartTime = startTime,
            EndTime = endTime,
            Status = status,
        }, ct);

    /// <summary>
    /// Stablecoin order list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcStableCoinOrderPage>> GetStableCoinOrdersAsync(GateOtcStableCoinOrderListRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("page_size", request.PageSize);
        parameters.AddOptional("page_number", request.PageNumber);
        parameters.AddOptional("coin_name", request.CoinName);
        parameters.AddOptional("start_time", FormatTime(request.StartTime));
        parameters.AddOptional("end_time", FormatTime(request.EndTime));
        parameters.AddOptional("status", request.Status);

        return SendOtcDataRequestAsync<GateOtcStableCoinOrderPage>("stable_coin/order/list", HttpMethod.Get, ct, parameters);
    }

    /// <summary>
    /// Fiat order details
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcFiatOrderDetail>> GetFiatOrderAsync(long orderId, CancellationToken ct = default)
        => GetFiatOrderAsync(orderId.ToString(CultureInfo.InvariantCulture), ct);

    /// <summary>
    /// Fiat order details
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcFiatOrderDetail>> GetFiatOrderAsync(string orderId, CancellationToken ct = default)
        => GetFiatOrderAsync(new GateOtcOrderIdRequest { OrderId = orderId }, ct);

    /// <summary>
    /// Fiat order details
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcFiatOrderDetail>> GetFiatOrderAsync(GateOtcOrderIdRequest request, CancellationToken ct = default)
    {
        Require(request.OrderId, nameof(request.OrderId));

        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId },
        };

        return SendOtcDataRequestAsync<GateOtcFiatOrderDetail>("order/detail", HttpMethod.Get, ct, parameters);
    }
}
