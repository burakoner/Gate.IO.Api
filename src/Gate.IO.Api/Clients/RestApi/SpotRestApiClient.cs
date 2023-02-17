using Gate.IO.Api.Models.RestApi;
using Gate.IO.Api.Models.RestApi.Spot;

namespace Gate.IO.Api.Clients.RestApi;

public class SpotRestApiClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string spot = "spot";

    // Endpoints
    private const string timeEndpoint = "time";
    private const string currenciesEndpoint = "currencies";
    private const string currencyPairsEndpoint = "currency_pairs";
    private const string tickersEndpoint = "tickers";
    private const string orderbookEndpoint = "order_book";
    private const string tradesEndpoint = "trades";
    private const string candlesticksEndpoint = "candlesticks";
    private const string feeEndpoint = "fee";
    private const string batchFeeEndpoint = "batch_fee";
    private const string accountsEndpoint = "accounts";
    private const string ordersEndpoint = "orders";
    private const string batchOrdersEndpoint = "batch_orders";
    private const string openOrdersEndpoint = "open_orders";
    private const string crossLiquidateOrdersEndpoint = "cross_liquidate_orders";
    private const string cancelBatchOrdersEndpoint = "cancel_batch_orders";
    private const string myTradesEndpoint = "my_trades";
    private const string countdownCancelAllEndpoint = "countdown_cancel_all";
    private const string priceOrdersEndpoint = "price_orders";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO Spot RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal SpotRestApiClient(GateRestApiClient root) : base("Gate.IO Spot RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new GateAuthenticationProvider(credentials);

    protected override CallError ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    public override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal async Task<RestCallResult<T>> SendRequestInternal<T>(
        Uri uri,
        HttpMethod method,
        CancellationToken cancellationToken,
        bool signed = false,
        Dictionary<string, object> queryParameters = null,
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null,
        ArraySerialization? arraySerialization = null,
        JsonSerializer deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }
    #endregion

    #region Get server current time
    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await SendRequestInternal<SpotTime>(RootClient.GetUrl(api, version, spot, timeEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }
    #endregion

    #region List all currencies' details
    public async Task<RestCallResult<IEnumerable<SpotCurrency>>> GetAllCurrenciesAsync(CancellationToken ct = default)
    {
        return await SendRequestInternal<IEnumerable<SpotCurrency>>(RootClient.GetUrl(api, version, spot, currenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Get details of a specific currency
    public async Task<RestCallResult<SpotCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return await SendRequestInternal<SpotCurrency>(RootClient.GetUrl(api, version, spot, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region List all currency pairs supported
    public async Task<RestCallResult<IEnumerable<SpotMarket>>> GetAllPairsAsync(CancellationToken ct = default)
    {
        return await SendRequestInternal<IEnumerable<SpotMarket>>(RootClient.GetUrl(api, version, spot, currencyPairsEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Get details of a specific currency pair
    public async Task<RestCallResult<SpotMarket>> GetPairAsync(string symbol, CancellationToken ct = default)
    {
        return await SendRequestInternal<SpotMarket>(RootClient.GetUrl(api, version, spot, currencyPairsEndpoint.AppendPath(symbol)), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve ticker information
    public async Task<RestCallResult<IEnumerable<SpotTicker>>> GetTickersAsync(string symbol = "", TickerTimezone? timezone = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
        };
        parameters.AddOptionalParameter("timezone", JsonConvert.SerializeObject(timezone, new TickerTimezoneConverter(false)));

        return await SendRequestInternal<IEnumerable<SpotTicker>>(RootClient.GetUrl(api, version, spot, tickersEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve order book
    public async Task<RestCallResult<SpotOrderBook>> GetOrderBookAsync(string symbol, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        return await SendRequestInternal<SpotOrderBook>(RootClient.GetUrl(api, version, spot, orderbookEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve market trades
    public async Task<RestCallResult<IEnumerable<SpotTrade>>> GetTradesAsync(string symbol, DateTime from, DateTime to, int limit = 100, long? lastId = null, bool reverse = false, int page = 1, CancellationToken ct = default)
    => await GetTradesAsync(symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, lastId, reverse, page, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<SpotTrade>>> GetTradesAsync(string symbol, long? from = null, long? to = null, int limit = 100, long? lastId = null, bool reverse = false, int page = 1, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
            { "limit", limit },
            { "reverse", reverse.ToString().ToLower() },
            { "page", page },
        };
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<SpotTrade>>(RootClient.GetUrl(api, version, spot, tradesEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Market candlesticks
    public async Task<RestCallResult<IEnumerable<SpotCandlestick>>> GetCandlesticksAsync(string symbol, SpotCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetCandlesticksAsync(symbol, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<SpotCandlestick>>> GetCandlesticksAsync(string symbol, SpotCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
            { "interval", JsonConvert.SerializeObject(interval, new SpotCandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<SpotCandlestick>>(RootClient.GetUrl(api, version, spot, candlesticksEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query user trading fee rates
    /// <summary>
    /// Query user trading fee rates
    /// This API is deprecated in favour of new fee retrieving API /wallet/fee.
    /// </summary>
    /// <param name="symbol">Specify a currency pair to retrieve precise fee rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<UserTradingFee>> GetUserFeeRatesAsync(string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await SendRequestInternal<UserTradingFee>(RootClient.GetUrl(api, version, spot, feeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query a batch of user trading fee rates
    /// <summary>
    /// Query a batch of user trading fee rates
    /// </summary>
    /// <param name="symbols">A request can only query up to 50 currency pairs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<Dictionary<string, UserTradingFee>>> GetUserFeeRatesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
    {
        if (symbols.Count() > 50) throw new ArgumentException("A request can only query up to 50 currency pairs");

        var parameters = new Dictionary<string, object> {
            { "currency_pairs", string.Join(",", symbols) }
        };

        return await SendRequestInternal<Dictionary<string, UserTradingFee>>(RootClient.GetUrl(api, version, spot, batchFeeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List spot accounts
    public async Task<RestCallResult<IEnumerable<SpotAccount>>> GetAccountAsync(string currency = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return await SendRequestInternal<IEnumerable<SpotAccount>>(RootClient.GetUrl(api, version, spot, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Create an order
    public async Task<RestCallResult<SpotOrder>> PlaceOrderAsync(
        AccountType account,
        string symbol,
        SpotOrderType type,
        SpotOrderSide side,
        SpotTimeInForce timeInForce,
        decimal amount,
        decimal? price = null,
        decimal? iceberg = null,
        bool? autoBorrow = null,
        bool? autoRepay = null,
        string clientOrderId = "",
        CancellationToken ct = default)
        => await PlaceOrderAsync(new SpotOrderRequest
        {
            Account = account,
            Symbol = symbol,
            Type = type,
            Side = side,
            TimeInForce = timeInForce,
            Amount = amount,
            Price = price,
            Iceberg = iceberg,
            AutoBorrow = autoBorrow,
            AutoRepay = autoRepay,
            ClientOrderId = clientOrderId,
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<SpotOrder>> PlaceOrderAsync(SpotOrderRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        if (request.Type == SpotOrderType.Market && request.TimeInForce == SpotTimeInForce.GoodTillCancelled)
            throw new ArgumentException("GTC (GoodTillCancelled) is not supported for market orders");

        if (request.Type != SpotOrderType.Market && request.TimeInForce == SpotTimeInForce.ImmediateOrCancel)
            throw new ArgumentException("IOC (ImmediateOrCancel) is only supported for market orders");

        if (request.Type != SpotOrderType.Market && request.TimeInForce == SpotTimeInForce.FillOrKill)
            throw new ArgumentException("FOK (FillOrKill) is only supported for market orders");

        if (request.AutoBorrow.HasValue && request.AutoBorrow.Value &&
            request.AutoRepay.HasValue && request.AutoRepay.Value)
            throw new ArgumentException("AutoBorrow and AutoRepay cannot be both set to true in one order.");

        var parameters = new Dictionary<string, object> {
            { "currency_pair", request.Symbol },
            { "side", JsonConvert.SerializeObject(request.Side, new SpotOrderSideConverter(false)) },
            { "type", JsonConvert.SerializeObject(request.Type, new SpotOrderTypeConverter(false)) },
            { "account", JsonConvert.SerializeObject(request.Account, new AccountTypeConverter(false)) },
            { "time_in_force", JsonConvert.SerializeObject(request.TimeInForce, new SpotTimeInForceConverter(false)) },
            { "amount", request.Amount },
        };
        parameters.AddOptionalParameter("text", request.ClientOrderId);
        parameters.AddOptionalParameter("iceberg", request.Iceberg);
        parameters.AddOptionalParameter("price", request.Price);
        parameters.AddOptionalParameter("auto_borrow", request.AutoBorrow);
        parameters.AddOptionalParameter("auto_repay", request.AutoRepay);

        return await SendRequestInternal<SpotOrder>(RootClient.GetUrl(api, version, spot, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Create a batch of orders
    public async Task<RestCallResult<IEnumerable<SpotBatchOrder>>> PlaceBatchOrdersAsync(IEnumerable<SpotOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
        {
            SpotHelpers.ValidateMarketSymbol(request.Symbol);
            ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, false);

            if (request.Type == SpotOrderType.Market && request.TimeInForce == SpotTimeInForce.GoodTillCancelled)
                throw new ArgumentException("GTC (GoodTillCancelled) is not supported for market orders");

            if (request.Type != SpotOrderType.Market && request.TimeInForce == SpotTimeInForce.ImmediateOrCancel)
                throw new ArgumentException("IOC (ImmediateOrCancel) is only supported for market orders");

            if (request.Type != SpotOrderType.Market && request.TimeInForce == SpotTimeInForce.FillOrKill)
                throw new ArgumentException("FOK (FillOrKill) is only supported for market orders");

            if (request.AutoBorrow.HasValue && request.AutoBorrow.Value &&
                request.AutoRepay.HasValue && request.AutoRepay.Value)
                throw new ArgumentException("AutoBorrow and AutoRepay cannot be both set to true in one order.");
        }

        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, requests },
        };

        return await SendRequestInternal<IEnumerable<SpotBatchOrder>>(RootClient.GetUrl(api, version, spot, batchOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List all open orders
    public async Task<RestCallResult<IEnumerable<SpotOpenOrders>>> GetOpenOrdersAsync(AccountType account, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "page", page },
            { "limit", limit },
            { "account", JsonConvert.SerializeObject(account, new AccountTypeConverter(false)) },
        };

        return await SendRequestInternal<IEnumerable<SpotOpenOrders>>(RootClient.GetUrl(api, version, spot, openOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Close position when cross-currency is disabled
    public async Task<RestCallResult<SpotOrder>> CloseLiquidatedPositionsAsync(SpotOrderRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new Dictionary<string, object> {
            { "currency_pair", request.Symbol },
            { "side", JsonConvert.SerializeObject(request.Side, new SpotOrderSideConverter(false)) },
            { "type", JsonConvert.SerializeObject(request.Type, new SpotOrderTypeConverter(false)) },
            { "account", JsonConvert.SerializeObject(request.Account, new AccountTypeConverter(false)) },
            { "time_in_force", JsonConvert.SerializeObject(request.TimeInForce, new SpotTimeInForceConverter(false)) },
            { "amount", request.Amount },
        };
        parameters.AddOptionalParameter("text", request.ClientOrderId);
        parameters.AddOptionalParameter("iceberg", request.Iceberg);
        parameters.AddOptionalParameter("price", request.Price);
        parameters.AddOptionalParameter("auto_borrow", request.AutoBorrow);
        parameters.AddOptionalParameter("auto_repay", request.AutoRepay);

        return await SendRequestInternal<SpotOrder>(RootClient.GetUrl(api, version, spot, crossLiquidateOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List orders
    public async Task<RestCallResult<IEnumerable<SpotOrder>>> GetOrdersAsync(
    AccountType account,
    string symbol,
    SpotOrderStatus status,
    SpotOrderSide side,
    DateTime from,
    DateTime to,
    int page = 1,
    int limit = 100,
    CancellationToken ct = default)
        => await GetOrdersAsync(account, symbol, status, side, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<SpotOrder>>> GetOrdersAsync(
        AccountType account,
        string symbol,
        SpotOrderStatus status,
        SpotOrderSide? side = null,
        long? from = null,
        long? to = null,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "account", JsonConvert.SerializeObject(account, new AccountTypeConverter(false)) },
            { "currency_pair", symbol },
            { "status", JsonConvert.SerializeObject(status, new SpotOrderStatusConverter(false)) },
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("side", JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)));
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<SpotOrder>>(RootClient.GetUrl(api, version, spot, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Cancel a single order
    public async Task<RestCallResult<SpotOrder>> CancelOrderAsync(
        AccountType account,
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new Dictionary<string, object> {
            { "account", JsonConvert.SerializeObject(account, new AccountTypeConverter(false)) },
            { "currency_pair", symbol },
        };

        var oid = orderId.HasValue ? orderId.Value.ToString(CI) : clientOrderId;
        return await SendRequestInternal<SpotOrder>(RootClient.GetUrl(api, version, spot, ordersEndpoint.AppendPath(oid)), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Cancel all open orders in specified currency pair
    public async Task<RestCallResult<IEnumerable<SpotOrder>>> CancelOrdersAsync(
        AccountType account,
        string symbol,
        SpotOrderSide? side = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new Dictionary<string, object> {
            { "account", JsonConvert.SerializeObject(account, new AccountTypeConverter(false)) },
            { "currency_pair", symbol },
        };
        parameters.AddOptionalParameter("side", JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)));

        return await SendRequestInternal<IEnumerable<SpotOrder>>(RootClient.GetUrl(api, version, spot, ordersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Cancel a batch of orders with an ID list
    public async Task<RestCallResult<SpotBatchOrder>> CancelOrdersAsync(IEnumerable<BatchCancelOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
            SpotHelpers.ValidateMarketSymbol(request.Symbol);

        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, requests },
        };

        return await SendRequestInternal<SpotBatchOrder>(RootClient.GetUrl(api, version, spot, cancelBatchOrdersEndpoint), HttpMethod.Post, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get a single order
    public async Task<RestCallResult<SpotOrder>> GetOrderAsync(
        AccountType account,
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new Dictionary<string, object> {
            { "account", JsonConvert.SerializeObject(account, new AccountTypeConverter(false)) },
            { "currency_pair", symbol },
        };

        var oid = orderId.HasValue ? orderId.Value.ToString(CI) : clientOrderId;
        return await SendRequestInternal<SpotOrder>(RootClient.GetUrl(api, version, spot, ordersEndpoint.AppendPath(oid)), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Amend an order
#if NETSTANDARD2_1
    public async Task<RestCallResult<SpotOrder>> AmendOrderAsync(
        AccountType account,
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        decimal? amount = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("amount", amount);
        parameters.AddOptionalParameter("price", price);

        var oid = orderId.HasValue ? orderId.Value.ToString(CI) : clientOrderId;
        var uri = RootClient.GetUrl(api, version, spot, ordersEndpoint.AppendPath(oid));
        uri = uri.AddQueryParmeter("currency_pair", symbol);

        return await SendRequestInternal<SpotOrder>(uri, HttpMethod.Patch, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
#endif
    #endregion

    #region List personal trading history
    public async Task<RestCallResult<IEnumerable<SpotUserTrade>>> GetUserTradesAsync(
    AccountType account,
    string symbol,
    DateTime from,
    DateTime to,
    int page = 1,
    int limit = 100,
    long? orderId = null,
    string clientOrderId = null,
    CancellationToken ct = default)
        => await GetUserTradesAsync(account, symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, orderId, clientOrderId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<SpotUserTrade>>> GetUserTradesAsync(
        AccountType? account = null,
        string symbol = "",
        long? from = null,
        long? to = null,
        int page = 1,
        int limit = 100,
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var oid = orderId.HasValue ? orderId.Value.ToString(CI) : clientOrderId;
        var parameters = new Dictionary<string, object>
        {
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("account", JsonConvert.SerializeObject(account, new AccountTypeConverter(false)));
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("order_id", oid);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<SpotUserTrade>>(RootClient.GetUrl(api, version, spot, myTradesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Countdown cancel orders
    public async Task<RestCallResult<DateTime>> CountdownCancelOrdersAsync(
        int timeout,
        string symbol = "",
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new Dictionary<string, object> {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);
        var result = await SendRequestInternal<SpotCountdown>(RootClient.GetUrl(api, version, spot, countdownCancelAllEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }
    #endregion

    #region Create a price-triggered order
    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        string symbol,
        decimal triggerPrice,
        PriceTriggerCondition triggerCondition,
        TimeSpan triggerExpiration,
        AccountType2 orderAccount,
        SpotOrderType orderType,
        SpotOrderSide orderSide,
        SpotTriggerOrderTimeInForce orderTimeInForce,
        decimal? orderAmount,
        decimal? orderPrice,
        CancellationToken ct = default)
        => await PlacePriceTriggeredOrderAsync(new SpotTriggerOrderRequest
        {
            Symbol = symbol,
            Trigger = new SpotPriceTrigger
            {
                Price = triggerPrice.ToString(CI),
                Rule = triggerCondition,
                Expiration = Convert.ToInt32(triggerExpiration.TotalSeconds),
            },
            Order = new SpotPriceOrder
            {
                Account = orderAccount,
                Type = orderType,
                Side = orderSide,
                TimeInForce = orderTimeInForce,
                Amount = orderAmount?.ToString(CI),
                Price = orderPrice?.ToString(CI)
            }
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(SpotTriggerOrderRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);

        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, request },
        };

        var result = await SendRequestInternal<SpotTriggerOrderId>(RootClient.GetUrl(api, version, spot, priceOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        return result.As(result.Data?.OrderId ?? default);
    }
    #endregion

    #region Retrieve running auto order list
    public async Task<RestCallResult<IEnumerable<SpotTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(
    AccountType2 account,
    PriceTriggerFilter status,
    string symbol = "",
    int limit = 100,
    int offset = 0,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "account", JsonConvert.SerializeObject(account, new AccountType2Converter(false)) },
            { "status", JsonConvert.SerializeObject(status, new PriceTriggerFilterConverter(false)) },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("market", symbol);

        return await SendRequestInternal<IEnumerable<SpotTriggerOrderResponse>>(RootClient.GetUrl(api, version, spot, priceOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Cancel all open orders
    public async Task<RestCallResult<IEnumerable<SpotTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(AccountType2? account = null, string symbol = "", CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("market", symbol);
        if (account.HasValue)
            parameters.AddOptionalParameter("account", JsonConvert.SerializeObject(account, new AccountType2Converter(false)));

        return await SendRequestInternal<IEnumerable<SpotTriggerOrderResponse>>(RootClient.GetUrl(api, version, spot, priceOrdersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get a price-triggered order
    public async Task<RestCallResult<SpotTriggerOrderResponse>> GetPriceTriggeredOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var oid = orderId.HasValue ? orderId.Value.ToString(CI) : clientOrderId;
        return await SendRequestInternal<SpotTriggerOrderResponse>(RootClient.GetUrl(api, version, spot, priceOrdersEndpoint.AppendPath(oid)), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Cancel a price-triggered order
    public async Task<RestCallResult<SpotTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var oid = orderId.HasValue ? orderId.Value.ToString(CI) : clientOrderId;
        return await SendRequestInternal<SpotTriggerOrderResponse>(RootClient.GetUrl(api, version, spot, priceOrdersEndpoint.AppendPath(oid)), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }
    #endregion
}