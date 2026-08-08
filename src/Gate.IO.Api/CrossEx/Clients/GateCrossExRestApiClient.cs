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
    public Task<RestCallResult<List<GateCrossExSymbol>>> GetSymbolsAsync(GateCrossExSymbolsQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(request.Symbols));

        return _.SendRequestInternal<List<GateCrossExSymbol>>(_.GetUrl(api, v4, crossex, "rule/symbols"), HttpMethod.Get, ct, queryParameters: parameters);
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
    /// Fund transfer
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
    /// Fund transfer
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExTransferResult>> TransferAsync(GateCrossExTransferRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "coin", request.Coin },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddEnum("from", request.From);
        parameters.AddEnum("to", request.To);
        parameters.AddOptional("text", request.Text);

        return _.SendRequestInternal<GateCrossExTransferResult>(_.GetUrl(api, v4, crossex, "transfers"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Create an order
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
    /// Create an order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> PlaceOrderAsync(GateCrossExOrderRequest request, CancellationToken ct = default)
    {
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

        return _.SendRequestInternal<GateCrossExOrderActionResult>(_.GetUrl(api, v4, crossex, "orders"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query order details
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
    /// Flash swap quote
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
    /// Flash swap quote
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExConvertQuote>> GetConvertQuoteAsync(GateCrossExConvertQuoteRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "from_coin", request.FromCoin },
            { "to_coin", request.ToCoin },
        };
        parameters.AddEnum("exchange_type", request.ExchangeType);
        parameters.AddString("from_amount", request.FromAmount);

        return _.SendRequestInternal<GateCrossExConvertQuote>(_.GetUrl(api, v4, crossex, "convert/quote"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    /// Full close position
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="positionSide">Position side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> ClosePositionAsync(string symbol, GateCrossExPositionSide? positionSide = null, CancellationToken ct = default)
        => ClosePositionAsync(new GateCrossExClosePositionRequest { Symbol = symbol, PositionSide = positionSide }, ct);

    /// <summary>
    /// Full close position
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossExOrderActionResult>> ClosePositionAsync(GateCrossExClosePositionRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "symbol", request.Symbol },
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
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossExHistoricalPosition>>> GetHistoricalPositionsAsync(GateCrossExHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddHistoryParameters(parameters, request);

        return _.SendRequestInternal<List<GateCrossExHistoricalPosition>>(_.GetUrl(api, v4, crossex, "history_positions"), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    public Task<RestCallResult<List<GateCrossExAccountBookRecord>>> GetAccountBookAsync(GateCrossExAccountBookQueryRequest request, CancellationToken ct = default)
    {
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
}
