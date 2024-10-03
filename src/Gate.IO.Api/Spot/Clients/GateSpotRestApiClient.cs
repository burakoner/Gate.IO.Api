namespace Gate.IO.Api.Spot;

/// <summary>
/// Gate.IO Spot REST API Client
/// </summary>
public class GateSpotRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string spot = "spot";

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

    // Root Client
    internal GateRestApiClient Root { get; }

    internal GateSpotRestApiClient(GateRestApiClient root)
    {
        Root = root;
    }

    /// <summary>
    /// List all currencies' details
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<List<GateSpotCurrency>>(Root.GetUrl(api, version, spot, currenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get details of a specific currency
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSpotCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<GateSpotCurrency>(Root.GetUrl(api, version, spot, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// List all currency pairs supported
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotMarket>>> GetMarketsAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<List<GateSpotMarket>>(Root.GetUrl(api, version, spot, currencyPairsEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get details of a specific currency pair
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSpotMarket>> GetMarketAsync(string symbol, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<GateSpotMarket>(Root.GetUrl(api, version, spot, currencyPairsEndpoint.AppendPath(symbol)), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve ticker information
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="timezone"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotTicker>>> GetTickersAsync(string symbol = "", GateSpotTickerTimezone? timezone = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("timezone", timezone);

        return await Root.SendRequestInternal<List<GateSpotTicker>>(Root.GetUrl(api, version, spot, tickersEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve order book
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="limit"></param>
    /// <param name="withId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSpotOrderBook>> GetOrderBookAsync(string symbol, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        return await Root.SendRequestInternal<GateSpotOrderBook>(Root.GetUrl(api, version, spot, orderbookEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve market trades
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="lastId"></param>
    /// <param name="reverse"></param>
    /// <param name="page"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotTrade>>> GetTradesAsync(string symbol, DateTime from, DateTime to, int limit = 100, long? lastId = null, bool reverse = false, int page = 1, CancellationToken ct = default)
    => await GetTradesAsync(symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, lastId, reverse, page, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve market trades
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="lastId"></param>
    /// <param name="reverse"></param>
    /// <param name="page"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotTrade>>> GetTradesAsync(string symbol, long? from = null, long? to = null, int limit = 100, long? lastId = null, bool reverse = false, int page = 1, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
            { "reverse", reverse },
            { "limit", limit },
            { "page", page },
        };
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var sign = Root.ClientOptions.AuthenticationProvider != null;
        //var sign = Root.ClientOptions.ApiCredentials != null &&
        //    Root.ClientOptions.ApiCredentials.Key != null &&
        //    Root.ClientOptions.ApiCredentials.Secret != null;

        return await Root.SendRequestInternal<List<GateSpotTrade>>(Root.GetUrl(api, version, spot, tradesEndpoint), HttpMethod.Get, ct, sign, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Market candlesticks
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotCandlestick>>> GetCandlesticksAsync(string symbol, GateSpotCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetCandlesticksAsync(symbol, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Market candlesticks
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotCandlestick>>> GetCandlesticksAsync(string symbol, GateSpotCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency_pair", symbol);
        parameters.AddEnum("interval", interval);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        if (!from.HasValue && !to.HasValue) parameters.AddOptional("limit", limit);

        return await Root.SendRequestInternal<List<GateSpotCandlestick>>(Root.GetUrl(api, version, spot, candlesticksEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query user trading fee rates
    /// This API is deprecated in favour of new fee retrieving API /wallet/fee.
    /// </summary>
    /// <param name="symbol">Specify a currency pair to retrieve precise fee rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSpotUserTradingFee>> GetUserFeeRatesAsync(string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<GateSpotUserTradingFee>(Root.GetUrl(api, version, spot, feeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query a batch of user trading fee rates
    /// </summary>
    /// <param name="symbols">A request can only query up to 50 currency pairs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<Dictionary<string, GateSpotUserTradingFee>>> GetUserFeeRatesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
    {
        if (symbols.Count() > 50) throw new ArgumentException("A request can only query up to 50 currency pairs");

        var parameters = new Dictionary<string, object> {
            { "currency_pairs", string.Join(",", symbols) }
        };

        return await Root.SendRequestInternal<Dictionary<string, GateSpotUserTradingFee>>(Root.GetUrl(api, version, spot, batchFeeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List spot accounts
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotBalance>>> GetBalancesAsync(string currency = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return await Root.SendRequestInternal<List<GateSpotBalance>>(Root.GetUrl(api, version, spot, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Query account book

    /// <summary>
    /// Create a batch of orders
    /// </summary>
    /// <param name="requests"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<List<GateSpotBatchOrder>>> PlaceOrdersAsync(IEnumerable<GateSpotOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
        {
            SpotHelpers.ValidateMarketSymbol(request.Symbol);
            ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, false);

            if (request.Type == GateSpotOrderType.Market && request.TimeInForce == GateSpotTimeInForce.GoodTillCancelled)
                throw new ArgumentException("GTC (GoodTillCancelled) is not supported for market orders");

            if (request.Type != GateSpotOrderType.Market && request.TimeInForce == GateSpotTimeInForce.ImmediateOrCancel)
                throw new ArgumentException("IOC (ImmediateOrCancel) is only supported for market orders");

            if (request.Type != GateSpotOrderType.Market && request.TimeInForce == GateSpotTimeInForce.FillOrKill)
                throw new ArgumentException("FOK (FillOrKill) is only supported for market orders");

            if (request.AutoBorrow.HasValue && request.AutoBorrow.Value &&
                request.AutoRepay.HasValue && request.AutoRepay.Value)
                throw new ArgumentException("AutoBorrow and AutoRepay cannot be both set to true in one order.");
        }

        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        return await Root.SendRequestInternal<List<GateSpotBatchOrder>>(Root.GetUrl(api, version, spot, batchOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List all open orders
    /// </summary>
    /// <param name="account"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotOpenOrders>>> GetOpenOrdersAsync(GateSpotAccountType? account = null, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalEnum("account", account);


        return await Root.SendRequestInternal<List<GateSpotOpenOrders>>(Root.GetUrl(api, version, spot, openOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Close position when cross-currency is disabled
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSpotOrder>> CloseLiquidatedPositionsAsync(GateSpotCloseRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new ParameterCollection()
        {
            { "currency_pair", request.Symbol },
            { "amount", request.Amount.ToGateString() },
            { "price", request.Price.ToGateString() },
        };
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalEnum("action_mode", request.ProcessingMode);

        return await Root.SendRequestInternal<GateSpotOrder>(Root.GetUrl(api, version, spot, crossLiquidateOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="account"></param>
    /// <param name="symbol"></param>
    /// <param name="type"></param>
    /// <param name="side"></param>
    /// <param name="timeInForce"></param>
    /// <param name="amount"></param>
    /// <param name="price"></param>
    /// <param name="iceberg"></param>
    /// <param name="autoBorrow"></param>
    /// <param name="autoRepay"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="stpAction"></param>
    /// <param name="actionMode"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSpotOrder>> PlaceOrderAsync(
        string symbol,
        GateSpotAccountType account,
        GateSpotOrderType type,
        GateSpotOrderSide side,
        GateSpotTimeInForce timeInForce,
        decimal amount,
        decimal? price = null,
        decimal? iceberg = null,
        bool? autoBorrow = null,
        bool? autoRepay = null,
        string clientOrderId = "",
        GateSpotSelfTradingPreventionAction? stpAction = null,
        GateSpotActionMode? actionMode = null,
        CancellationToken ct = default)
        => await PlaceOrderAsync(new GateSpotOrderRequest
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
            SelfTradingPreventionAction = stpAction,
            ActionMode = actionMode,
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSpotOrder>> PlaceOrderAsync(GateSpotOrderRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        if (request.Type == GateSpotOrderType.Market && request.TimeInForce == GateSpotTimeInForce.GoodTillCancelled)
            throw new ArgumentException("GTC (GoodTillCancelled) is not supported for market orders");

        if (request.Type != GateSpotOrderType.Market && request.TimeInForce == GateSpotTimeInForce.ImmediateOrCancel)
            throw new ArgumentException("IOC (ImmediateOrCancel) is only supported for market orders");

        if (request.Type != GateSpotOrderType.Market && request.TimeInForce == GateSpotTimeInForce.FillOrKill)
            throw new ArgumentException("FOK (FillOrKill) is only supported for market orders");

        if (request.AutoBorrow.HasValue && request.AutoBorrow.Value &&
            request.AutoRepay.HasValue && request.AutoRepay.Value)
            throw new ArgumentException("AutoBorrow and AutoRepay cannot be both set to true in one order.");

        var parameters = new ParameterCollection()
        {
            { "currency_pair", request.Symbol },
            { "amount", request.Amount.ToGateString() },
        };

        parameters.AddEnum("side", request.Side);
        parameters.AddEnum("type", request.Type);
        parameters.AddEnum("account", request.Account);
        parameters.AddEnum("time_in_force", request.TimeInForce);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptional("price", request.Price?.ToGateString());
        parameters.AddOptional("iceberg", request.Iceberg);
        parameters.AddOptional("auto_borrow", request.AutoBorrow);
        parameters.AddOptional("auto_repay", request.AutoRepay);
        parameters.AddOptionalEnum("stp_act", request.SelfTradingPreventionAction);
        parameters.AddOptionalEnum("action_mode", request.ActionMode);

        return await Root.SendRequestInternal<GateSpotOrder>(Root.GetUrl(api, version, spot, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /*
    public async Task<RestCallResult<List<SpotOrder>>> GetOrdersAsync(
    string symbol,
    GateSpotOrderQueryStatus status,
    GateSpotAccountType? account = GateSpotAccountType.Spot,
    GateSpotOrderSide? side = null,
    DateTime? from = null,
    DateTime? to = null,
    int page = 1,
    int limit = 100,
    CancellationToken ct = default)
        => await GetOrdersAsync(symbol, status, account, side, from?.ConvertToMilliseconds(), to?.ConvertToMilliseconds(), page, limit, ct).ConfigureAwait(false);
    */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="status"></param>
    /// <param name="account"></param>
    /// <param name="side"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotOrder>>> GetOrdersAsync(
        string symbol,
        GateSpotOrderQueryStatus status,
        GateSpotAccountType? account = null,
        GateSpotOrderSide? side = null,
        long? from = null,
        long? to = null,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "currency_pair", symbol },
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalEnum("status", status);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptionalEnum("side", side);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);

        return await Root.SendRequestInternal<List<GateSpotOrder>>(Root.GetUrl(api, version, spot, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel all open orders in specified currency pair
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="account"></param>
    /// <param name="side"></param>
    /// <param name="actionMode"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotOrder>>> CancelOrdersAsync(
        string symbol = null,
        GateSpotAccountType? account = null,
        GateSpotOrderSide? side = null,
        GateSpotActionMode? actionMode = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptionalEnum("side", side);
        parameters.AddOptionalEnum("action_mode", actionMode);

        return await Root.SendRequestInternal<List<GateSpotOrder>>(Root.GetUrl(api, version, spot, ordersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel a batch of orders with an ID list
    /// </summary>
    /// <param name="requests"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotCancelOrder>>> CancelOrdersAsync(IEnumerable<GateSpotCancelOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
            SpotHelpers.ValidateMarketSymbol(request.Symbol);

        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        return await Root.SendRequestInternal<List<GateSpotCancelOrder>>(Root.GetUrl(api, version, spot, cancelBatchOrdersEndpoint), HttpMethod.Post, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get a single order
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="account"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSpotOrder>> GetOrderAsync(
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        GateSpotAccountType? account = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);
        
        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (orderId.HasValue && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Only of of orderId and origClientOrderId can be sent");

        var parameters = new ParameterCollection()
        {
            { "currency_pair", symbol },
        };
        parameters.AddOptionalEnum("account", account);

        var oid = orderId.HasValue ? orderId.Value.ToString() : clientOrderId;
        return await Root.SendRequestInternal<GateSpotOrder>(Root.GetUrl(api, version, spot, ordersEndpoint.AppendPath(oid)), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

#if NETSTANDARD2_1
    /// <summary>
    /// Amend an order
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="amount"></param>
    /// <param name="price"></param>
    /// <param name="account"></param>
    /// <param name="amendText"></param>
    /// <param name="actionMode"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSpotOrder>> AmendOrderAsync(
        string symbol= null,
        long? orderId = null,
        string clientOrderId = null,
        decimal? amount = null,
        decimal? price = null,
        string amendText = null,
        GateSpotAccountType? account = null,
        GateSpotActionMode? actionMode = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (orderId.HasValue && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Only of of orderId and origClientOrderId can be sent");

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptionalString("amount", amount);
        parameters.AddOptionalString("price", price);
        parameters.AddOptional("amend_text", amendText);
        parameters.AddOptionalEnum("action_mode", actionMode);

        var oid = orderId.HasValue ? orderId.Value.ToString() : clientOrderId;
        var uri = Root.GetUrl(api, version, spot, ordersEndpoint.AppendPath(oid));
        if(!string.IsNullOrEmpty(symbol)) uri = uri.AddQueryParmeter("currency_pair", symbol);
        if(account!=null) uri = uri.AddQueryParmeter("account", MapConverter.GetString(account));

        return await Root.SendRequestInternal<GateSpotOrder>(uri, HttpMethod.Patch, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
#endif

    /// <summary>
    /// Cancel a single order
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="account"></param>
    /// <param name="actionMode"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSpotOrder>> CancelOrderAsync(
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        GateSpotAccountType? account = null,
        GateSpotActionMode? actionMode = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (orderId.HasValue && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Only of of orderId and origClientOrderId can be sent");

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptionalEnum("action_mode", actionMode);

        var oid = orderId.HasValue ? orderId.Value.ToString() : clientOrderId;
        return await Root.SendRequestInternal<GateSpotOrder>(Root.GetUrl(api, version, spot, ordersEndpoint.AppendPath(oid)), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List personal trading history
    /// </summary>
    /// <param name="account"></param>
    /// <param name="symbol"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotTradeHistory>>> GetTradeHistoryAsync(
    GateSpotAccountType account,
    string symbol,
    DateTime from,
    DateTime to,
    int page = 1,
    int limit = 100,
    long? orderId = null,
    string clientOrderId = null,
    CancellationToken ct = default)
        => await GetTradeHistoryAsync(account, symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, orderId, clientOrderId, ct).ConfigureAwait(false);

    /// <summary>
    /// List personal trading history
    /// </summary>
    /// <param name="account"></param>
    /// <param name="symbol"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<List<GateSpotTradeHistory>>> GetTradeHistoryAsync(
        GateSpotAccountType? account = null,
        string symbol = "",
        long? from = null,
        long? to = null,
        int page = 1,
        int limit = 100,
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrEmpty(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);
        
        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (orderId.HasValue && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Only of of orderId and origClientOrderId can be sent");

        var oid = orderId.HasValue ? orderId.Value.ToString() : clientOrderId;
        var parameters = new ParameterCollection
        {
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("order_id", oid);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);

        return await Root.SendRequestInternal<List<GateSpotTradeHistory>>(Root.GetUrl(api, version, spot, myTradesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Get server current time
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await Root.SendRequestInternal<GateSpotTime>(Root.GetUrl(api, version, spot, timeEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }

    /// <summary>
    /// Countdown cancel orders
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="symbol"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> CancelAllAsync(
        int timeout,
        string symbol = "",
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new Dictionary<string, object> {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);
        var result = await Root.SendRequestInternal<GateSpotCountdown>(Root.GetUrl(api, version, spot, countdownCancelAllEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }

    // TODO: Batch modification of orders

    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="triggerPrice"></param>
    /// <param name="triggerCondition"></param>
    /// <param name="triggerExpiration"></param>
    /// <param name="orderAccount"></param>
    /// <param name="orderType"></param>
    /// <param name="orderSide"></param>
    /// <param name="orderTimeInForce"></param>
    /// <param name="orderAmount"></param>
    /// <param name="orderPrice"></param>
    /// <param name="orderClientOrderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        string symbol,
        decimal triggerPrice,
        GateSpotTriggerCondition triggerCondition,
        TimeSpan triggerExpiration,
        GateSpotAccountType orderAccount,
        GateSpotOrderType orderType,
        GateSpotOrderSide orderSide,
        GateSpotTriggerTimeInForce orderTimeInForce,
        decimal? orderAmount,
        decimal? orderPrice,
        string orderClientOrderId = "",
        CancellationToken ct = default)
        => await PlacePriceTriggeredOrderAsync(new GateSpotPriceTriggeredOrderRequest
        {
            Symbol = symbol,
            Trigger = new GateSpotTriggerPrice
            {
                Price = triggerPrice.ToGateString(),
                Rule = triggerCondition,
                Expiration = Convert.ToInt32(triggerExpiration.TotalSeconds),
            },
            Order = new GateSpotTriggerOrder
            {
                Account = orderAccount,
                Type = orderType,
                Side = orderSide,
                TimeInForce = orderTimeInForce,
                Amount = orderAmount?.ToGateString(),
                Price = orderPrice?.ToGateString(),
                ClientOrderId = orderClientOrderId,
            }
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateSpotPriceTriggeredOrderRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var result = await Root.SendRequestInternal<GateSpotPriceTriggeredOrderId>(Root.GetUrl(api, version, spot, priceOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        return result.As(result.Data?.OrderId ?? default);
    }

    /// <summary>
    /// Retrieve running auto order list
    /// </summary>
    /// <param name="account"></param>
    /// <param name="status"></param>
    /// <param name="symbol"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSpotPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(
    GateSpotTriggerFilter status,
    GateSpotAccountType? account = null,
    string symbol = "",
    int limit = 100,
    int offset = 0,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("status", status);
        parameters.AddOptional("market", symbol);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        return await Root.SendRequestInternal<List<GateSpotPriceTriggeredOrder>>(Root.GetUrl(api, version, spot, priceOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel all open orders
    /// </summary>
    /// <param name="account"></param>
    /// <param name="symbol"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<GateSpotPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(
        GateSpotAccountType? account = null, 
        string symbol = "", 
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new ParameterCollection();
        parameters.AddOptional("market", symbol);
        parameters.AddOptionalEnum("account", account);

        return await Root.SendRequestInternal<IEnumerable<GateSpotPriceTriggeredOrder>>(Root.GetUrl(api, version, spot, priceOrdersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get a price-triggered order
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSpotPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (orderId.HasValue && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Only of of orderId and origClientOrderId can be sent");

        var oid = orderId.HasValue ? orderId.Value.ToString() : clientOrderId;
        return await Root.SendRequestInternal<GateSpotPriceTriggeredOrder>(Root.GetUrl(api, version, spot, priceOrdersEndpoint.AppendPath(oid)), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel a price-triggered order
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSpotPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (orderId.HasValue && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Only of of orderId and origClientOrderId can be sent");

        var oid = orderId.HasValue ? orderId.Value.ToString() : clientOrderId;
        return await Root.SendRequestInternal<GateSpotPriceTriggeredOrder>(Root.GetUrl(api, version, spot, priceOrdersEndpoint.AppendPath(oid)), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }
}