namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Gate.IO CrossEx REST API client
/// </summary>
public class GateCrossExRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string crossex = "crossex";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateCrossExRestApiClient(GateRestApiClient root) => _ = root;

    private static void ValidateRequiredValue(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A nonblank value is required", name);
    }

    private static void ValidateInstructionEnum<T>(T value, string name) where T : struct
    {
        if (!Enum.IsDefined(typeof(T), value)) throw new ArgumentOutOfRangeException(name, "Undefined CrossEx instruction enum");
    }

    private static string JoinValues(IEnumerable<string> values)
        => values == null ? null : string.Join(",", values.Where(x => !string.IsNullOrWhiteSpace(x)));

    private static void AddPaging(ParameterCollection parameters, int? page, int? limit)
    {
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);
    }

    private static void AddMilliseconds(ParameterCollection parameters, DateTime? from, DateTime? to)
    {
        parameters.AddOptional("from", from?.ConvertToMilliseconds());
        parameters.AddOptional("to", to?.ConvertToMilliseconds());
    }

    private static void AddSeconds(ParameterCollection parameters, DateTime? from, DateTime? to)
    {
        parameters.AddOptional("from", from?.ConvertToSeconds());
        parameters.AddOptional("to", to?.ConvertToSeconds());
    }

    private static void AddHistoryParameters(ParameterCollection parameters, GateCrossExHistoryQueryRequest request)
    {
        AddPaging(parameters, request.Page, request.Limit);
        parameters.AddOptional("symbol", request.Symbol);
        AddMilliseconds(parameters, request.From, request.To);
    }

    private static void AddPositionParameters(ParameterCollection parameters, GateCrossExPositionQueryRequest request)
    {
        parameters.AddOptional("symbol", request.Symbol);
        parameters.AddOptionalEnum("exchange_type", request.ExchangeType);
    }

    private static void AddCoinExchangeParameters(ParameterCollection parameters, GateCrossExCoinExchangeQueryRequest request)
    {
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptionalEnum("exchange_type", request.ExchangeType);
    }

    /// <summary>
    /// Query trading pair information
    /// </summary>
    /// <param name="symbols">Trading pair list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExSymbol>>> GetSymbolsAsync(IEnumerable<string> symbols = null, CancellationToken ct = default)
        => GetSymbolsAsync(new GateCrossExSymbolsQueryRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Query trading pair information
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateCrossExSymbol>>> GetSymbolsAsync(GateCrossExSymbolsQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        var symbols = request.Symbols?.ToList();
        if (symbols != null && symbols.Any(x => string.IsNullOrWhiteSpace(x) || x.Any(char.IsWhiteSpace) || x.Any(char.IsControl) || x.Contains(',')))
            throw new ArgumentException("Each symbol must be one nonblank value, not an embedded CSV list", nameof(request.Symbols));

        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", symbols == null || symbols.Count == 0 ? null : string.Join(",", symbols));

        var result = await _.SendRequestInternal<List<GateCrossExSymbol>>(_.GetUrl(api, v4, crossex, "rule/symbols"), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (result.Success && result.Data == null)
            return result.AsError<List<GateCrossExSymbol>>(new DeserializeError("Expected a CrossEx symbols array", result.Data));
        return result;
    }

    /// <summary>
    /// Query risk limit information for futures/margin trading pairs
    /// </summary>
    /// <param name="symbols">Trading pair list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExRiskLimit>>> GetRiskLimitsAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        => GetRiskLimitsAsync(new GateCrossExRiskLimitQueryRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Query risk limit information for futures/margin trading pairs
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExRiskLimit>>> GetRiskLimitsAsync(GateCrossExRiskLimitQueryRequest request, CancellationToken ct = default)
    {
        var symbols = JoinValues(request.Symbols);
        if (string.IsNullOrWhiteSpace(symbols))
            throw new ArgumentException("Symbols must contain at least one value", nameof(request.Symbols));

        var parameters = new ParameterCollection
        {
            { "symbols", symbols },
        };

        return _.SendRequestInternal<List<GateCrossExRiskLimit>>(_.GetUrl(api, v4, crossex, "rule/risk_limits"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query supported transfer currencies
    /// </summary>
    /// <param name="coin">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExTransferCoin>>> GetTransferCoinsAsync(string coin = null, CancellationToken ct = default)
        => GetTransferCoinsAsync(new GateCrossExTransferCoinQueryRequest { Coin = coin }, ct);

    /// <summary>
    /// Query supported transfer currencies
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExTransferCoin>>> GetTransferCoinsAsync(GateCrossExTransferCoinQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("coin", request.Coin);

        return _.SendRequestInternal<List<GateCrossExTransferCoin>>(_.GetUrl(api, v4, crossex, "transfers/coin"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query fund transfer history
    /// </summary>
    /// <param name="coin">Currency</param>
    /// <param name="orderId">Order ID or client-defined ID</param>
    /// <param name="from">Start time</param>
    /// <param name="to">End time</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum records, max 1000</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExTransferRecord>>> GetTransferHistoryAsync(string coin = null, string orderId = null, DateTime? from = null, DateTime? to = null, int? page = null, int? limit = null, CancellationToken ct = default)
        => GetTransferHistoryAsync(new GateCrossExTransferHistoryQueryRequest { Coin = coin, OrderId = orderId, From = from, To = to, Page = page, Limit = limit }, ct);

    /// <summary>
    /// Query fund transfer history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExTransferRecord>>> GetTransferHistoryAsync(GateCrossExTransferHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptional("order_id", request.OrderId);
        AddSeconds(parameters, request.From, request.To);
        AddPaging(parameters, request.Page, request.Limit);

        return _.SendRequestInternal<List<GateCrossExTransferRecord>>(_.GetUrl(api, v4, crossex, "transfers"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Fund transfer. LIGHTER supports USDC between CROSSEX_LIGHTER and SPOT.
    /// Rate limit: 10 requests per 10 seconds. No retry or follow-up transfer is performed automatically.
    /// </summary>
    /// <param name="coin">Currency</param>
    /// <param name="amount">Transfer amount</param>
    /// <param name="from">Source account</param>
    /// <param name="to">Destination account</param>
    /// <param name="text">Client-defined ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExTransferResult>> TransferAsync(string coin, decimal amount, GateCrossExTransferAccountType from, GateCrossExTransferAccountType to, string text = null, CancellationToken ct = default)
        => TransferAsync(new GateCrossExTransferRequest { Coin = coin, Amount = amount, From = from, To = to, Text = text }, ct);

    /// <summary>
    /// Fund transfer. LIGHTER supports USDC between CROSSEX_LIGHTER and SPOT.
    /// Rate limit: 10 requests per 10 seconds. No retry or follow-up transfer is performed automatically.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateCrossExTransferResult>> TransferAsync(GateCrossExTransferRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        ValidateRequiredValue(request.Coin, nameof(request.Coin));
        ValidateInstructionEnum(request.From, nameof(request.From));
        ValidateInstructionEnum(request.To, nameof(request.To));

        var parameters = new ParameterCollection
        {
            { "coin", request.Coin },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddEnum("from", request.From);
        parameters.AddEnum("to", request.To);
        parameters.AddOptional("text", request.Text);

        var result = await _.SendRequestInternal<GateCrossExTransferResult>(_.GetUrl(api, v4, crossex, "transfers"), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        if (result.Success && (result.Data == null || string.IsNullOrWhiteSpace(result.Data.TransactionId)))
            return result.AsError<GateCrossExTransferResult>(new DeserializeError("Expected a CrossEx transfer acknowledgement with a transaction ID", result.Data));
        return result;
    }

    /// <summary>
    /// Create an order. A successful response only acknowledges that CrossEx accepted the asynchronous request.
    /// Query the order or subscribe to private order updates to confirm venue acceptance and execution.
    /// Rate limit: 100 requests per 10 seconds; at most 1,000 open orders per user.
    /// </summary>
    /// <param name="symbol">Trading pair identifier</param>
    /// <param name="side">Order side</param>
    /// <param name="type">Order type</param>
    /// <param name="timeInForce">Time in force</param>
    /// <param name="quantity">Base currency order quantity</param>
    /// <param name="price">Limit order price</param>
    /// <param name="quoteQuantity">Quote currency order quantity</param>
    /// <param name="reduceOnly">Reduce-only flag</param>
    /// <param name="positionSide">Position side</param>
    /// <param name="text">Client-defined order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> PlaceOrderAsync(
        string symbol,
        GateCrossExOrderSide side,
        GateCrossExOrderType? type = null,
        GateCrossExTimeInForce? timeInForce = null,
        decimal? quantity = null,
        decimal? price = null,
        decimal? quoteQuantity = null,
        bool? reduceOnly = null,
        GateCrossExPositionSide? positionSide = null,
        string text = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(new GateCrossExOrderRequest
        {
            Symbol = symbol,
            Side = side,
            Type = type,
            TimeInForce = timeInForce,
            Quantity = quantity,
            Price = price,
            QuoteQuantity = quoteQuantity,
            ReduceOnly = reduceOnly,
            PositionSide = positionSide,
            Text = text,
        }, ct);

    /// <summary>
    /// Create an order. A successful response only acknowledges that CrossEx accepted the asynchronous request.
    /// Query the order or subscribe to private order updates to confirm venue acceptance and execution.
    /// Rate limit: 100 requests per 10 seconds; at most 1,000 open orders per user.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateCrossExOrderActionResult>> PlaceOrderAsync(GateCrossExOrderRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        ValidateRequiredValue(request.Symbol, nameof(request.Symbol));
        ValidateInstructionEnum(request.Side, nameof(request.Side));
        if (request.Type.HasValue) ValidateInstructionEnum(request.Type.Value, nameof(request.Type));
        if (request.TimeInForce.HasValue) ValidateInstructionEnum(request.TimeInForce.Value, nameof(request.TimeInForce));
        if (request.PositionSide.HasValue) ValidateInstructionEnum(request.PositionSide.Value, nameof(request.PositionSide));
        if (request.Text != null && (request.Text.Length >= 64 || !Regex.IsMatch(request.Text, @"\A[a-z0-9_-]+\z")))
            throw new ArgumentException("Order text must be shorter than 64 characters using only a-z, 0-9, hyphen and underscore", nameof(request.Text));
        if (request.Quantity.HasValue && request.Quantity.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(request.Quantity), "Order quantity must be greater than zero");
        if (request.QuoteQuantity.HasValue && request.QuoteQuantity.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(request.QuoteQuantity), "Quote quantity must be greater than zero");
        if (request.Type == GateCrossExOrderType.Market
            && (request.TimeInForce == GateCrossExTimeInForce.PendingOrCancelled || request.TimeInForce == GateCrossExTimeInForce.RetailPriceImprovement))
            throw new ArgumentException("Market orders cannot use POC or RPI", nameof(request.TimeInForce));

        var symbolParts = request.Symbol.Split('_');
        if (symbolParts.Length >= 4 && symbolParts[1] == "MARGIN"
            && request.PositionSide != GateCrossExPositionSide.Long && request.PositionSide != GateCrossExPositionSide.Short)
            throw new ArgumentException("Margin orders require an explicit LONG or SHORT position side", nameof(request.PositionSide));
        var quoteRequired = request.Type == GateCrossExOrderType.Market && request.Side == GateCrossExOrderSide.Buy
            && symbolParts.Length >= 4 && (symbolParts[1] == "SPOT" || symbolParts[1] == "MARGIN");
        if (quoteRequired && !request.QuoteQuantity.HasValue)
            throw new ArgumentException("Spot and margin market buys require quote quantity", nameof(request.QuoteQuantity));
        if (!quoteRequired && !request.Quantity.HasValue)
            throw new ArgumentException("This order requires base quantity", nameof(request.Quantity));
        if ((request.Type == null || request.Type == GateCrossExOrderType.Limit) && !request.Price.HasValue)
            throw new ArgumentException("Limit orders require a price", nameof(request.Price));

        var parameters = new ParameterCollection
        {
            { "symbol", request.Symbol },
        };
        parameters.AddEnum("side", request.Side);
        parameters.AddOptional("text", request.Text);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptionalEnum("time_in_force", request.TimeInForce);
        parameters.AddOptionalString("qty", request.Quantity);
        parameters.AddOptionalString("price", request.Price);
        parameters.AddOptionalString("quote_qty", request.QuoteQuantity);
        parameters.AddOptional("reduce_only", request.ReduceOnly.HasValue ? request.ReduceOnly.Value.ToString().ToLowerInvariant() : null);
        parameters.AddOptionalEnum("position_side", request.PositionSide);

        var result = await _.SendRequestInternal<GateCrossExOrderActionResult>(_.GetUrl(api, v4, crossex, "orders"), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        if (result.Success && (result.Data == null || string.IsNullOrWhiteSpace(result.Data.OrderId)))
            return result.AsError<GateCrossExOrderActionResult>(new DeserializeError("Expected a CrossEx order acknowledgement with an order ID", result.Data));
        return result;
    }

    /// <summary>
    /// Query order details, including the final CrossEx validation or venue rejection state and reason
    /// </summary>
    /// <param name="orderId">Order ID or client-defined order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        => _.SendRequestInternal<GateCrossExOrder>(_.GetUrl(api, v4, crossex, $"orders/{orderId}"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Modify order
    /// </summary>
    /// <param name="orderId">Order ID or client-defined order ID</param>
    /// <param name="quantity">Modified quantity</param>
    /// <param name="price">Modified price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> UpdateOrderAsync(string orderId, decimal? quantity = null, decimal? price = null, CancellationToken ct = default)
        => UpdateOrderAsync(orderId, new GateCrossExOrderUpdateRequest { Quantity = quantity, Price = price }, ct);

    /// <summary>
    /// Modify order
    /// </summary>
    /// <param name="orderId">Order ID or client-defined order ID</param>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> UpdateOrderAsync(string orderId, GateCrossExOrderUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("qty", request.Quantity);
        parameters.AddOptionalString("price", request.Price);

        return _.SendRequestInternal<GateCrossExOrderActionResult>(_.GetUrl(api, v4, crossex, $"orders/{orderId}"), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel order
    /// </summary>
    /// <param name="orderId">Order ID or client-defined order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        => _.SendRequestInternal<GateCrossExOrderActionResult>(_.GetUrl(api, v4, crossex, $"orders/{orderId}"), HttpMethod.Delete, ct, true);

    /// <summary>
    /// Cancel multiple specified orders. Each item must provide an order ID or custom text; the order ID takes precedence when both are provided.
    /// Rate limit: 100 requests per 10 seconds.
    /// </summary>
    /// <param name="requests">Order cancellation request items</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<List<GateCrossExBatchCancelOrderResult>>> CancelOrdersAsync(IEnumerable<GateCrossExBatchCancelOrderRequest> requests, CancellationToken ct = default)
    {
        if (requests == null)
            throw new ArgumentNullException(nameof(requests));

        var requestList = requests.ToList();
        if (requestList.Any(x => x == null || x.OrderId == null && x.Text == null))
            throw new ArgumentException("Each batch cancellation item must provide an order ID or custom text", nameof(requests));

        var parameters = new ParameterCollection();
        parameters.SetBody(requestList);

        return _.SendRequestInternal<List<GateCrossExBatchCancelOrderResult>>(_.GetUrl(api, v4, crossex, "batch_cancel_orders"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Flash swap quote only (100 requests per day). LIGHTER_USDC / CROSSEX_USDT swaps require CROSS_EXCHANGE mode.
    /// No quote execution, expiry inference or automatic account-mode change is performed.
    /// </summary>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="fromCoin">Asset sold</param>
    /// <param name="toCoin">Asset bought</param>
    /// <param name="fromAmount">Amount to sell</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExConvertQuote>> GetConvertQuoteAsync(GateCrossExExchangeType exchangeType, string fromCoin, string toCoin, decimal fromAmount, CancellationToken ct = default)
        => GetConvertQuoteAsync(new GateCrossExConvertQuoteRequest { ExchangeType = exchangeType, FromCoin = fromCoin, ToCoin = toCoin, FromAmount = fromAmount }, ct);

    /// <summary>
    /// Flash swap quote only (100 requests per day). LIGHTER_USDC / CROSSEX_USDT swaps require CROSS_EXCHANGE mode.
    /// No quote execution, expiry inference or automatic account-mode change is performed.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateCrossExConvertQuote>> GetConvertQuoteAsync(GateCrossExConvertQuoteRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        ValidateInstructionEnum(request.ExchangeType, nameof(request.ExchangeType));
        if (request.ExchangeType == GateCrossExExchangeType.CrossEx || request.ExchangeType == GateCrossExExchangeType.Deribit)
            throw new ArgumentOutOfRangeException(nameof(request.ExchangeType), "This exchange is not documented for CrossEx quotes");
        ValidateRequiredValue(request.FromCoin, nameof(request.FromCoin));
        ValidateRequiredValue(request.ToCoin, nameof(request.ToCoin));
        if (request.FromCoin == request.ToCoin)
            throw new ArgumentException("Quote source and destination assets must differ", nameof(request.ToCoin));
        if (request.FromAmount <= 0 || ((decimal.GetBits(request.FromAmount)[3] >> 16) & 0xff) > 16)
            throw new ArgumentOutOfRangeException(nameof(request.FromAmount), "Quote amount must be greater than zero with no more than 16 decimal places; it is never rounded");

        var parameters = new ParameterCollection
        {
            { "from_coin", request.FromCoin },
            { "to_coin", request.ToCoin },
        };
        parameters.AddEnum("exchange_type", request.ExchangeType);
        parameters.AddString("from_amount", request.FromAmount);

        var result = await _.SendRequestInternal<GateCrossExConvertQuote>(_.GetUrl(api, v4, crossex, "convert/quote"), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        if (result.Success && (result.Data == null || string.IsNullOrWhiteSpace(result.Data.QuoteId)
            || string.IsNullOrWhiteSpace(result.Data.FromCoin) || string.IsNullOrWhiteSpace(result.Data.ToCoin)))
            return result.AsError<GateCrossExConvertQuote>(new DeserializeError("Expected a CrossEx quote with an ID and both assets", result.Data));
        return result;
    }

    /// <summary>
    /// Flash swap transaction
    /// </summary>
    /// <param name="quoteId">Quote ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExConvertOrderResult>> CreateConvertOrderAsync(string quoteId, CancellationToken ct = default)
        => CreateConvertOrderAsync(new GateCrossExConvertOrderRequest { QuoteId = quoteId }, ct);

    /// <summary>
    /// Flash swap transaction
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExConvertOrderResult>> CreateConvertOrderAsync(GateCrossExConvertOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "quote_id", request.QuoteId },
        };

        return _.SendRequestInternal<GateCrossExConvertOrderResult>(_.GetUrl(api, v4, crossex, "convert/orders"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query account assets
    /// </summary>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExAccount>> GetAccountAsync(GateCrossExExchangeType? exchangeType = null, CancellationToken ct = default)
        => GetAccountAsync(new GateCrossExAccountQueryRequest { ExchangeType = exchangeType }, ct);

    /// <summary>
    /// Query account assets
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExAccount>> GetAccountAsync(GateCrossExAccountQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("exchange_type", request.ExchangeType);

        return _.SendRequestInternal<GateCrossExAccount>(_.GetUrl(api, v4, crossex, "accounts"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Modify account contract position mode and account mode
    /// </summary>
    /// <param name="positionMode">Position mode</param>
    /// <param name="accountMode">Account mode</param>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExAccountUpdateResult>> UpdateAccountAsync(GateCrossExPositionMode? positionMode = null, GateCrossExAccountMode? accountMode = null, GateCrossExExchangeType? exchangeType = null, CancellationToken ct = default)
        => UpdateAccountAsync(new GateCrossExAccountUpdateRequest { PositionMode = positionMode, AccountMode = accountMode, ExchangeType = exchangeType }, ct);

    /// <summary>
    /// Modify account contract position mode and account mode
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExAccountUpdateResult>> UpdateAccountAsync(GateCrossExAccountUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("position_mode", request.PositionMode);
        parameters.AddOptionalEnum("account_mode", request.AccountMode);
        parameters.AddOptionalEnum("exchange_type", request.ExchangeType);

        return _.SendRequestInternal<GateCrossExAccountUpdateResult>(_.GetUrl(api, v4, crossex, "accounts"), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query contract trading pair leverage multipliers
    /// </summary>
    /// <param name="symbols">Trading pair list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<Dictionary<string, decimal>>> GetContractLeveragesAsync(IEnumerable<string> symbols = null, CancellationToken ct = default)
        => GetContractLeveragesAsync(new GateCrossExLeverageQueryRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Query contract trading pair leverage multipliers
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<Dictionary<string, decimal>>> GetContractLeveragesAsync(GateCrossExLeverageQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(request.Symbols));

        return _.SendRequestInternal<Dictionary<string, decimal>>(_.GetUrl(api, v4, crossex, "positions/leverage"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Modify contract trading pair leverage multiplier
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExLeverageResult>> UpdateContractLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        => UpdateContractLeverageAsync(new GateCrossExLeverageRequest { Symbol = symbol, Leverage = leverage }, ct);

    /// <summary>
    /// Modify contract trading pair leverage multiplier
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExLeverageResult>> UpdateContractLeverageAsync(GateCrossExLeverageRequest request, CancellationToken ct = default)
        => SendLeverageUpdateAsync("positions/leverage", request, ct);

    /// <summary>
    /// Query margin trading pair leverage multipliers
    /// </summary>
    /// <param name="symbols">Trading pair list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<Dictionary<string, decimal>>> GetMarginLeveragesAsync(IEnumerable<string> symbols = null, CancellationToken ct = default)
        => GetMarginLeveragesAsync(new GateCrossExLeverageQueryRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Query margin trading pair leverage multipliers
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<Dictionary<string, decimal>>> GetMarginLeveragesAsync(GateCrossExLeverageQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(request.Symbols));

        return _.SendRequestInternal<Dictionary<string, decimal>>(_.GetUrl(api, v4, crossex, "margin_positions/leverage"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Modify margin trading pair leverage multiplier
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExLeverageResult>> UpdateMarginLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        => UpdateMarginLeverageAsync(new GateCrossExLeverageRequest { Symbol = symbol, Leverage = leverage }, ct);

    /// <summary>
    /// Modify margin trading pair leverage multiplier
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExLeverageResult>> UpdateMarginLeverageAsync(GateCrossExLeverageRequest request, CancellationToken ct = default)
        => SendLeverageUpdateAsync("margin_positions/leverage", request, ct);

    private Task<RestCallResult<GateCrossExLeverageResult>> SendLeverageUpdateAsync(string endpoint, GateCrossExLeverageRequest request, CancellationToken ct)
    {
        var parameters = new ParameterCollection
        {
            { "symbol", request.Symbol },
        };
        parameters.AddString("leverage", request.Leverage);

        return _.SendRequestInternal<GateCrossExLeverageResult>(_.GetUrl(api, v4, crossex, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get one futures symbol's margin mode. Signed GET; documented rate: 200 requests per 10 seconds.
    /// https://www.gate.com/docs/developers/apiv4/en/crossex/#get-futures-position-margin-mode
    /// </summary>
    /// <param name="symbol">Required single futures symbol. No Hyperliquid-only restriction is imposed on GET.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Server symbol and raw mode, without inferring a default or updating local account state.</returns>
    public Task<RestCallResult<GateCrossExMarginModeResponse>> GetMarginModeAsync(string symbol, CancellationToken ct = default)
        => GetMarginModeAsync(new GateCrossExMarginModeQueryRequest { Symbol = symbol }, ct);

    /// <summary>Query one required futures symbol. No automatic mode mutation, retry or other account lookup.</summary>
    /// <param name="request">Single-symbol query.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Server symbol and raw mode, including unknown future mode strings.</returns>
    public Task<RestCallResult<GateCrossExMarginModeResponse>> GetMarginModeAsync(GateCrossExMarginModeQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        ValidateRequiredValue(request.Symbol, nameof(request.Symbol));
        if (request.Symbol.Any(char.IsWhiteSpace) || request.Symbol.Any(char.IsControl) || request.Symbol.Contains(','))
            throw new ArgumentException("A single futures symbol is required", nameof(request.Symbol));
        var parameters = new ParameterCollection { { "symbol", request.Symbol } };
        return SendMarginModeRequestAsync(HttpMethod.Get, parameters, null, ct);
    }

    /// <summary>
    /// Explicitly change one Hyperliquid futures symbol's margin mode. Signed POST; HTTP 202 is acceptance only.
    /// Documented rate: 100 requests per 10 seconds. Open orders or positions prevent the change.
    /// https://www.gate.com/docs/developers/apiv4/en/crossex/#update-futures-position-margin-mode
    /// </summary>
    /// <param name="symbol">Required Hyperliquid futures symbol.</param>
    /// <param name="marginMode">Explicit CROSS/ISOLATED mode, with no default.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Server acknowledgement, not confirmation that the requested mode is active.</returns>
    public Task<RestCallResult<GateCrossExMarginModeResponse>> UpdateMarginModeAsync(string symbol, GateCrossExMarginMode marginMode, CancellationToken ct = default)
        => UpdateMarginModeAsync(new GateCrossExMarginModeRequest { Symbol = symbol, MarginMode = marginMode }, ct);

    /// <summary>
    /// Explicit Hyperliquid margin-mode update only. Eligibility and open-order/position state are checked by the server.
    /// No automatic cancellation, closing, account-mode change, margin adjustment, pre-query, retry or polling occurs.
    /// </summary>
    /// <param name="request">Required symbol and explicitly selected mode.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>HTTP acknowledgement with required server-returned symbol/mode; missing values are not filled from input.</returns>
    public Task<RestCallResult<GateCrossExMarginModeResponse>> UpdateMarginModeAsync(GateCrossExMarginModeRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Symbol)
            || !request.Symbol.StartsWith("HYPERLIQUID_FUTURE_", StringComparison.Ordinal)
            || request.Symbol.LastIndexOf('_') <= "HYPERLIQUID_FUTURE_".Length
            || request.Symbol.EndsWith("_", StringComparison.Ordinal)
            || request.Symbol.Any(char.IsWhiteSpace) || request.Symbol.Any(char.IsControl) || request.Symbol.Contains(','))
            throw new ArgumentException("A single Hyperliquid futures symbol is required", nameof(request.Symbol));
        ValidateInstructionEnum(request.MarginMode, nameof(request.MarginMode));
        var parameters = new ParameterCollection();
        parameters.SetBody(request);
        return SendMarginModeRequestAsync(HttpMethod.Post, null, parameters, ct);
    }

    private async Task<RestCallResult<GateCrossExMarginModeResponse>> SendMarginModeRequestAsync(
        HttpMethod method, ParameterCollection query, ParameterCollection body, CancellationToken ct)
    {
        var serializer = JsonSerializer.Create(new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, crossex, "positions/margin_mode"),
            method, ct, true, queryParameters: query, bodyParameters: body, deserializer: serializer).ConfigureAwait(false);
        if (!result.Success) return result.As<GateCrossExMarginModeResponse>(null);
        try
        {
            var token = result.Data;
            // RawResponse's dependency path may parse raw future mode strings as dates before invoking the serializer.
            if (!string.IsNullOrEmpty(result.Raw))
            {
                using var reader = new JsonTextReader(new System.IO.StringReader(result.Raw)) { DateParseHandling = DateParseHandling.None };
                token = serializer.Deserialize<JToken>(reader);
            }
            var response = token?.ToObject<GateCrossExMarginModeResponse>(serializer);
            if (response != null) return result.As(response);
        }
        catch (JsonException) { }
        return result.AsError<GateCrossExMarginModeResponse>(new DeserializeError("Invalid or incomplete margin-mode response", null));
    }

    /// <summary>
    /// Increase/decrease an existing Hyperliquid isolated futures position's margin. Signed POST; HTTP 202 is acceptance only.
    /// Documented rate limit: 100 requests per 10 seconds.
    /// No account-mode lookup, automatic margin-mode change, retry or completion confirmation is performed.
    /// </summary>
    /// <param name="symbol">Hyperliquid futures trading pair</param>
    /// <param name="margin">Signed adjustment. Sent unchanged as a string; the server truncates beyond two decimal places.</param>
    /// <param name="positionSide">Optional NONE/LONG/SHORT. Omission defaults to NONE for one-way positions on the server.</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<GateCrossExIsolatedMarginResponse>> UpdateIsolatedMarginAsync(string symbol, decimal margin, GateCrossExPositionSide? positionSide = null, CancellationToken ct = default)
        => UpdateIsolatedMarginAsync(new GateCrossExIsolatedMarginRequest { Symbol = symbol, Margin = margin, PositionSide = positionSide }, ct);

    /// <summary>
    /// Increase/decrease an existing Hyperliquid isolated futures position's margin. HTTP 202 acknowledges acceptance, not completion.
    /// Documented rate limit: 100 requests per 10 seconds.
    /// Eligibility, available margin and actual truncation are server-side; the supplied instructions are not rewritten.
    /// </summary>
    /// <param name="request">Explicit margin adjustment</param>
    /// <param name="ct">Cancellation Token</param>
    public async Task<RestCallResult<GateCrossExIsolatedMarginResponse>> UpdateIsolatedMarginAsync(GateCrossExIsolatedMarginRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Symbol)
            || !request.Symbol.StartsWith("HYPERLIQUID_FUTURE_", StringComparison.Ordinal)
            || request.Symbol.LastIndexOf('_') <= "HYPERLIQUID_FUTURE_".Length
            || request.Symbol.EndsWith("_", StringComparison.Ordinal)
            || request.Symbol.Any(char.IsWhiteSpace) || request.Symbol.Any(char.IsControl) || request.Symbol.Contains(','))
            throw new ArgumentException("A single Hyperliquid futures symbol is required", nameof(request.Symbol));
        if (request.PositionSide.HasValue && !Enum.IsDefined(typeof(GateCrossExPositionSide), request.PositionSide.Value))
            throw new ArgumentException("PositionSide must be NONE, LONG or SHORT", nameof(request.PositionSide));

        var parameters = new ParameterCollection();
        parameters.SetBody(request);
        var result = await _.SendRequestInternal<GateCrossExIsolatedMarginResponse>(_.GetUrl(api, v4, crossex, "positions/margin"), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        if (result.Success && result.Data == null)
            return result.AsError<GateCrossExIsolatedMarginResponse>(new DeserializeError("Expected an isolated-margin acknowledgement object", result.Data));
        return result;
    }

    /// <summary>
    /// Fully close a futures or margin position that is strictly below either the minimum notional amount or the minimum order size.
    /// The account must not have an open order for the symbol.
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="positionSide">Position side. Required for margin positions and optional for futures positions depending on the position mode.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> ClosePositionAsync(string symbol, GateCrossExPositionSide? positionSide = null, CancellationToken ct = default)
        => ClosePositionAsync(new GateCrossExClosePositionRequest { Symbol = symbol, PositionSide = positionSide }, ct);

    /// <summary>
    /// Fully close a futures or margin position that is strictly below either the minimum notional amount or the minimum order size.
    /// The account must not have an open order for the symbol.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> ClosePositionAsync(GateCrossExClosePositionRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Symbol))
            throw new ArgumentException("Symbol is required", nameof(request.Symbol));
        if (request.PositionSide.HasValue && !Enum.IsDefined(typeof(GateCrossExPositionSide), request.PositionSide.Value))
            throw new ArgumentOutOfRangeException(nameof(request.PositionSide));

        var symbol = request.Symbol.Trim();
        if (IsMarginSymbol(symbol) && !request.PositionSide.HasValue)
            throw new ArgumentException("Position side is required for margin positions", nameof(request.PositionSide));

        var parameters = new ParameterCollection
        {
            { "symbol", symbol },
        };
        parameters.AddOptionalEnum("position_side", request.PositionSide);

        return _.SendRequestInternal<GateCrossExOrderActionResult>(_.GetUrl(api, v4, crossex, "position"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query margin asset interest rates
    /// </summary>
    /// <param name="coin">Currency</param>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExInterestRate>>> GetInterestRatesAsync(string coin = null, GateCrossExExchangeType? exchangeType = null, CancellationToken ct = default)
        => GetInterestRatesAsync(new GateCrossExCoinExchangeQueryRequest { Coin = coin, ExchangeType = exchangeType }, ct);

    /// <summary>
    /// Query margin asset interest rates
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExInterestRate>>> GetInterestRatesAsync(GateCrossExCoinExchangeQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddCoinExchangeParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExInterestRate>>(_.GetUrl(api, v4, crossex, "interest_rate"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query user fee rates
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExFee>>> GetFeesAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateCrossExFee>>(_.GetUrl(api, v4, crossex, "fee"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query contract positions
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExPosition>>> GetPositionsAsync(string symbol = null, GateCrossExExchangeType? exchangeType = null, CancellationToken ct = default)
        => GetPositionsAsync(new GateCrossExPositionQueryRequest { Symbol = symbol, ExchangeType = exchangeType }, ct);

    /// <summary>
    /// Query contract positions
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExPosition>>> GetPositionsAsync(GateCrossExPositionQueryRequest request, CancellationToken ct = default)
    {
        request ??= new GateCrossExPositionQueryRequest();

        var parameters = new ParameterCollection();
        AddPositionParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExPosition>>(_.GetUrl(api, v4, crossex, "positions"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query margin positions
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarginPosition>>> GetMarginPositionsAsync(string symbol = null, GateCrossExExchangeType? exchangeType = null, CancellationToken ct = default)
        => GetMarginPositionsAsync(new GateCrossExPositionQueryRequest { Symbol = symbol, ExchangeType = exchangeType }, ct);

    /// <summary>
    /// Query margin positions
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarginPosition>>> GetMarginPositionsAsync(GateCrossExPositionQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddPositionParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExMarginPosition>>(_.GetUrl(api, v4, crossex, "margin_positions"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query ADL position reduction ranking
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExAdlRank>>> GetAdlRankAsync(string symbol, CancellationToken ct = default)
        => GetAdlRankAsync(new GateCrossExAdlRankQueryRequest { Symbol = symbol }, ct);

    /// <summary>
    /// Query ADL position reduction ranking
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExAdlRank>>> GetAdlRankAsync(GateCrossExAdlRankQueryRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Symbol))
            throw new ArgumentException("Symbol is required", nameof(request.Symbol));

        var parameters = new ParameterCollection
        {
            { "symbol", request.Symbol },
        };

        return _.SendRequestInternal<List<GateCrossExAdlRank>>(_.GetUrl(api, v4, crossex, "adl_rank"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query current open orders
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="businessType">Business type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExOrder>>> GetOpenOrdersAsync(string symbol = null, GateCrossExExchangeType? exchangeType = null, GateCrossExBusinessType? businessType = null, CancellationToken ct = default)
        => GetOpenOrdersAsync(new GateCrossExOpenOrdersQueryRequest { Symbol = symbol, ExchangeType = exchangeType, BusinessType = businessType }, ct);

    /// <summary>
    /// Query current open orders
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExOrder>>> GetOpenOrdersAsync(GateCrossExOpenOrdersQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", request.Symbol);
        parameters.AddOptionalEnum("exchange_type", request.ExchangeType);
        parameters.AddOptionalEnum("business_type", request.BusinessType);

        return _.SendRequestInternal<List<GateCrossExOrder>>(_.GetUrl(api, v4, crossex, "open_orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query order history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExOrder>>> GetHistoricalOrdersAsync(GateCrossExHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddHistoryParameters(parameters, request);
        parameters.AddOptional("attributes", request.Attributes == null
            ? null
            : string.Join(",", request.Attributes.Select(MapConverter.GetString)));

        return _.SendRequestInternal<List<GateCrossExOrder>>(_.GetUrl(api, v4, crossex, "history_orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query contract position history
    /// Optional time filters use milliseconds; the documented maximum limit is 1000.
    /// Documented rate limit: 200 requests per 10 seconds.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateCrossExHistoricalPosition>>> GetHistoricalPositionsAsync(GateCrossExHistoryQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Limit > 1000) throw new ArgumentOutOfRangeException(nameof(request.Limit), "Maximum limit is 1000");
        // Normalize explicit local instants only. Preserve the legacy UTC interpretation of unspecified values.
        var from = request.From?.Kind == DateTimeKind.Local ? request.From.Value.ToUniversalTime() : request.From;
        var to = request.To?.Kind == DateTimeKind.Local ? request.To.Value.ToUniversalTime() : request.To;
        if (from.HasValue && to.HasValue && from.Value.ConvertToMilliseconds() > to.Value.ConvertToMilliseconds())
            throw new ArgumentException("From must not be later than To", nameof(request.From));
        if (request.Symbol != null && (string.IsNullOrWhiteSpace(request.Symbol) || request.Symbol.Any(char.IsWhiteSpace) || request.Symbol.Any(char.IsControl) || request.Symbol.Contains(',')))
            throw new ArgumentException("Symbol must be a single nonblank trading pair", nameof(request.Symbol));

        var parameters = new ParameterCollection();
        AddPaging(parameters, request.Page, request.Limit);
        parameters.AddOptional("symbol", request.Symbol);
        AddMilliseconds(parameters, from, to);

        var result = await _.SendRequestInternal<List<GateCrossExHistoricalPosition>>(_.GetUrl(api, v4, crossex, "history_positions"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (result.Success && result.Data == null)
            return result.AsError<List<GateCrossExHistoricalPosition>>(new DeserializeError("Expected a CrossEx historical positions array", result.Data));
        return result;
    }

    /// <summary>
    /// Query margin position history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExHistoricalMarginPosition>>> GetHistoricalMarginPositionsAsync(GateCrossExHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddHistoryParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExHistoricalMarginPosition>>(_.GetUrl(api, v4, crossex, "history_margin_positions"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query margin interest deduction history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarginInterestRecord>>> GetMarginInterestHistoryAsync(GateCrossExMarginInterestHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddHistoryParameters(parameters, request);
        parameters.AddOptionalEnum("exchange_type", request.ExchangeType);

        return _.SendRequestInternal<List<GateCrossExMarginInterestRecord>>(_.GetUrl(api, v4, crossex, "history_margin_interests"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query filled history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExTrade>>> GetTradeHistoryAsync(GateCrossExHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddHistoryParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExTrade>>(_.GetUrl(api, v4, crossex, "history_trades"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query account asset change history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExAccountBookRecord>>> GetAccountBookAsync(GateCrossExAccountBookQueryRequest request = null, CancellationToken ct = default)
    {
        request ??= new GateCrossExAccountBookQueryRequest();
        if (request.Limit > 1000)
            throw new ArgumentOutOfRangeException(nameof(request.Limit), "Limit cannot exceed 1000");

        var parameters = new ParameterCollection();
        AddPaging(parameters, request.Page, request.Limit);
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptional("statement_type", request.StatementType);
        AddMilliseconds(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateCrossExAccountBookRecord>>(_.GetUrl(api, v4, crossex, "account_book"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query coin discount rates
    /// </summary>
    /// <param name="coin">Currency</param>
    /// <param name="exchangeType">Exchange type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExCoinDiscountRate>>> GetCoinDiscountRatesAsync(string coin = null, GateCrossExExchangeType? exchangeType = null, CancellationToken ct = default)
        => GetCoinDiscountRatesAsync(new GateCrossExCoinExchangeQueryRequest { Coin = coin, ExchangeType = exchangeType }, ct);

    /// <summary>
    /// Query coin discount rates
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExCoinDiscountRate>>> GetCoinDiscountRatesAsync(GateCrossExCoinExchangeQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddCoinExchangeParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExCoinDiscountRate>>(_.GetUrl(api, v4, crossex, "coin_discount_rate"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get exchange market tickers. Margin trading pairs cannot be used as direct filters.
    /// Rate limit: 1 request per second.
    /// </summary>
    /// <param name="symbols">Optional trading pair list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarketTicker>>> GetMarketTickersAsync(IEnumerable<string> symbols = null, CancellationToken ct = default)
        => GetMarketTickersAsync(new GateCrossExSymbolsQueryRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Get exchange market tickers. Margin trading pairs cannot be used as direct filters.
    /// Rate limit: 1 request per second.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarketTicker>>> GetMarketTickersAsync(GateCrossExSymbolsQueryRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var symbols = request.Symbols?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        if (symbols?.Any(IsMarginSymbol) == true)
            throw new ArgumentException("Margin trading pairs cannot be used as ticker filters", nameof(request.Symbols));

        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(symbols));

        return _.SendRequestInternal<List<GateCrossExMarketTicker>>(_.GetUrl(api, v4, crossex, "market/tickers"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get exchange futures funding rate information.
    /// For Deribit, the funding rate is the current real-time rate calculated over an 8-hour period.
    /// Rate limit: 1 request per second.
    /// </summary>
    /// <param name="symbols">Optional trading pair list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarketFundingInfo>>> GetMarketFundingInfoAsync(IEnumerable<string> symbols = null, CancellationToken ct = default)
        => GetMarketFundingInfoAsync(new GateCrossExSymbolsQueryRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Get exchange futures funding rate information.
    /// For Deribit, the funding rate is the current real-time rate calculated over an 8-hour period.
    /// Rate limit: 1 request per second.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExMarketFundingInfo>>> GetMarketFundingInfoAsync(GateCrossExSymbolsQueryRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(request.Symbols));

        return _.SendRequestInternal<List<GateCrossExMarketFundingInfo>>(_.GetUrl(api, v4, crossex, "market/funding_info"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    private static bool IsMarginSymbol(string symbol)
    {
        var parts = symbol.Split('_');
        return parts.Length > 1 && string.Equals(parts[1], "MARGIN", StringComparison.OrdinalIgnoreCase);
    }
}
