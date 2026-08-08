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
    public Task<RestCallResult<GateOtcQuote>> GetQuoteAsync(GateOtcQuoteRequest request, CancellationToken ct = default)
    {
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

        return SendOtcDataRequestAsync<GateOtcQuote>("quote", HttpMethod.Post, ct, bodyParameters: parameters);
    }

    /// <summary>
    /// Create fiat order
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
    /// Create fiat order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> CreateFiatOrderAsync(GateOtcFiatOrderRequest request, CancellationToken ct = default)
    {
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

        return _.SendRequestInternal<GateOtcActionResult>(_.GetUrl(api, v4, otc, "order/create"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
        string payCoin = null,
        string getCoin = null,
        decimal? payAmount = null,
        decimal? getAmount = null,
        GateOtcQuoteSide? side = null,
        string quoteToken = null,
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
        var parameters = new ParameterCollection();
        parameters.AddOptional("pay_coin", request.PayCoin);
        parameters.AddOptional("get_coin", request.GetCoin);
        parameters.AddOptionalString("pay_amount", request.PayAmount);
        parameters.AddOptionalString("get_amount", request.GetAmount);
        parameters.AddOptionalEnum("side", request.Side);
        parameters.AddOptional("promotion_code", request.PromotionCode);
        parameters.AddOptional("quote_token", request.QuoteToken);

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
    /// Create bank card
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcBankCreateResult>> CreateBankCardAsync(GateOtcBankCreateRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.BankAccountName, nameof(request.BankAccountName));
        Require(request.BankName, nameof(request.BankName));
        Require(request.BankCountry, nameof(request.BankCountry));
        Require(request.BankAddress, nameof(request.BankAddress));
        Require(request.Iban, nameof(request.Iban));
        Require(request.Swift, nameof(request.Swift));
        Require(request.DocumentationFile, nameof(request.DocumentationFile));

        var form = new ParameterCollection
        {
            { "bank_account_name", request.BankAccountName },
            { "bank_name", request.BankName },
            { "bank_country", request.BankCountry },
            { "bank_address", request.BankAddress },
            { "iban", request.Iban },
            { "swift", request.Swift },
            { "documentation_file", request.DocumentationFile },
        };
        form.AddOptional("remittance_line_number", request.RemittanceLineNumber);
        form.AddOptional("agent_bank_name", request.AgentBankName);
        form.AddOptional("agent_bank_swift", request.AgentBankSwift);

        return SendOtcDataRequestAsync<GateOtcBankCreateResult>(
            "bank/create",
            HttpMethod.Post,
            ct,
            bodyParameters: GateMultipartFormData.CreateBodyParameters(form));
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
    /// Submit personal bank card supplementary materials
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> SubmitPersonalBankSupplementAsync(GateOtcBankPersonalSupplementRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.BankId, nameof(request.BankId));
        Require(request.IdDocumentFront, nameof(request.IdDocumentFront));
        Require(request.IdDocumentBack, nameof(request.IdDocumentBack));
        Require(request.AddressProof, nameof(request.AddressProof));

        var form = new ParameterCollection
        {
            { "bank_id", request.BankId },
            { "id_document_front", request.IdDocumentFront },
            { "id_document_back", request.IdDocumentBack },
            { "address_proof", request.AddressProof },
        };
        form.AddOptional("relationship_proof", request.RelationshipProof);

        return _.SendRequestInternal<GateOtcActionResult>(
            _.GetUrl(api, v4, otc, "bank/personal/bank_supplement"),
            HttpMethod.Post,
            ct,
            true,
            bodyParameters: GateMultipartFormData.CreateBodyParameters(form));
    }

    /// <summary>
    /// Submit enterprise bank card supplementary materials
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOtcActionResult>> SubmitEnterpriseBankSupplementAsync(GateOtcBankEnterpriseSupplementRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        Require(request.BankId, nameof(request.BankId));
        Require(request.Certificate, nameof(request.Certificate));
        Require(request.ShareHolders, nameof(request.ShareHolders));
        Require(request.Passport, nameof(request.Passport));
        Require(request.ShareHoldingStructure, nameof(request.ShareHoldingStructure));

        var form = new ParameterCollection
        {
            { "bank_id", request.BankId },
            { "certificate", request.Certificate },
            { "share_holders", request.ShareHolders },
            { "passport", request.Passport },
            { "share_holding_structure", request.ShareHoldingStructure },
        };
        form.AddOptional("uid", request.UserId);
        form.AddOptional("funds_statement", request.FundsStatement);
        form.AddOptional("additional", request.Additional);
        form.AddOptional("relationship_proof", request.RelationshipProof);

        return _.SendRequestInternal<GateOtcActionResult>(
            _.GetUrl(api, v4, otc, "bank/enterprise/bank_supplement"),
            HttpMethod.Post,
            ct,
            true,
            bodyParameters: GateMultipartFormData.CreateBodyParameters(form));
    }

    /// <summary>
    /// Mark fiat order as paid
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

        return _.SendRequestInternal<GateOtcActionResult>(_.GetUrl(api, v4, otc, "order/paid"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    /// <param name="type">BUY, SELL, or ALL</param>
    /// <param name="fiatCurrency">Fiat currency</param>
    /// <param name="cryptoCurrency">Digital currency</param>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="status">Order status</param>
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
