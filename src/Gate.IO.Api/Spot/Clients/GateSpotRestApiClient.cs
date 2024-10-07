namespace Gate.IO.Api.Spot;

/// <summary>
/// Gate.IO Spot REST API Client
/// </summary>
public class GateSpotRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string section = "spot";

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
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateSpotRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// List all currencies' details
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateSpotCurrency>>(_.GetUrl(api, version, section, currenciesEndpoint), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Get details of a specific currency
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSpotCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateSpotCurrency>(_.GetUrl(api, version, section, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct);
    }

    /// <summary>
    /// List all currency pairs supported
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotMarket>>> GetMarketsAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateSpotMarket>>(_.GetUrl(api, version, section, currencyPairsEndpoint), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Get details of a specific currency pair
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSpotMarket>> GetMarketAsync(string symbol, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateSpotMarket>(_.GetUrl(api, version, section, currencyPairsEndpoint.AppendPath(symbol)), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Retrieve ticker information
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="timezone">Timezone</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotTicker>>> GetTickersAsync(string symbol = null, GateSpotTickerTimezone? timezone = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("timezone", timezone);

        return _.SendRequestInternal<List<GateSpotTicker>>(_.GetUrl(api, version, section, tickersEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve order book
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="interval">Order depth. 0 means no aggregation is applied. default to 0</param>
    /// <param name="limit">Maximum number of order depth data in asks or bids</param>
    /// <param name="withId">Return order book ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSpotOrderBook>> GetOrderBookAsync(string symbol, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency_pair", symbol },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        return _.SendRequestInternal<GateSpotOrderBook>(_.GetUrl(api, version, section, orderbookEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve market trades
    /// You can use from and to to query by time range, or use last_id by scrolling page. The default behavior is by time range, The query range is the last 30 days.
    /// Scrolling query using last_id is not recommended any more. If last_id is specified, time range query parameters will be ignored.
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list. Default: 100, Minimum: 1, Maximum: 1000</param>
    /// <param name="lastId">Specify list staring point using the id of last record in previous list-query results</param>
    /// <param name="reverse">Whether the id of records to be retrieved should be less than the last_id specified. Default to false.</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotTrade>>> GetTradesAsync(string symbol, DateTime from, DateTime to, int limit = 100, long? lastId = null, bool reverse = false, int page = 1, CancellationToken ct = default)
    => GetTradesAsync(symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, lastId, reverse, page, ct);
    
    /// <summary>
    /// Retrieve market trades
    /// You can use from and to to query by time range, or use last_id by scrolling page. The default behavior is by time range, The query range is the last 30 days.
    /// Scrolling query using last_id is not recommended any more. If last_id is specified, time range query parameters will be ignored.
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list. Default: 100, Minimum: 1, Maximum: 1000</param>
    /// <param name="lastId">Specify list staring point using the id of last record in previous list-query results</param>
    /// <param name="reverse">Whether the id of records to be retrieved should be less than the last_id specified. Default to false.</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotTrade>>> GetTradesAsync(string symbol, long? from = null, long? to = null, int limit = 100, long? lastId = null, bool reverse = false, int page = 1, CancellationToken ct = default)
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

        var sign = _.ClientOptions.AuthenticationProvider != null;
        //var sign = Root.ClientOptions.ApiCredentials != null &&
        //    Root.ClientOptions.ApiCredentials.Key != null &&
        //    Root.ClientOptions.ApiCredentials.Secret != null;

        return _.SendRequestInternal<List<GateSpotTrade>>(_.GetUrl(api, version, section, tradesEndpoint), HttpMethod.Get, ct, sign, queryParameters: parameters);
    }

    /// <summary>
    /// Market candlesticks
    /// Maximum of 1000 points can be returned in a query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="interval">Interval time between data points. Note that 30d means 1 natual month, not 30 days</param>
    /// <param name="from">Start time of candlesticks, formatted in Unix timestamp in seconds. Default toto - 100 * interval if not specified</param>
    /// <param name="to">End time of candlesticks, formatted in Unix timestamp in seconds. Default to current time</param>
    /// <param name="limit">Maximum recent data points to return. limit is conflicted with from and to. If either from or to is specified, request will be rejected.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotCandlestick>>> GetCandlesticksAsync(string symbol, GateSpotCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetCandlesticksAsync(symbol, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);
    
    /// <summary>
    /// Market candlesticks
    /// Maximum of 1000 points can be returned in a query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="interval">Interval time between data points. Note that 30d means 1 natual month, not 30 days</param>
    /// <param name="from">Start time of candlesticks, formatted in Unix timestamp in seconds. Default toto - 100 * interval if not specified</param>
    /// <param name="to">End time of candlesticks, formatted in Unix timestamp in seconds. Default to current time</param>
    /// <param name="limit">Maximum recent data points to return. limit is conflicted with from and to. If either from or to is specified, request will be rejected.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotCandlestick>>> GetCandlesticksAsync(string symbol, GateSpotCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency_pair", symbol);
        parameters.AddEnum("interval", interval);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        if (!from.HasValue && !to.HasValue) parameters.AddOptional("limit", limit);

        return _.SendRequestInternal<List<GateSpotCandlestick>>(_.GetUrl(api, version, section, candlesticksEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Query user trading fee rates
    /// This API is deprecated in favour of new fee retrieving API /wallet/fee.
    /// </summary>
    /// <param name="symbol">Specify a currency pair to retrieve precise fee rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    [Obsolete("This API is deprecated in favour of new fee retrieving API /wallet/fee")]
    public Task<RestCallResult<GateSpotUserTradingFee>> GetUserFeeRatesAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);

        return _.SendRequestInternal<GateSpotUserTradingFee>(_.GetUrl(api, version, section, feeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query a batch of user trading fee rates
    /// </summary>
    /// <param name="symbols">A request can only query up to 50 currency pairs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("This API is deprecated in favour of new fee retrieving API /wallet/fee")]
    public Task<RestCallResult<Dictionary<string, GateSpotUserTradingFee>>> GetUserFeeRatesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
    {
        if (symbols.Count() > 50) 
            throw new ArgumentException("A request can only query up to 50 currency pairs");

        var parameters = new Dictionary<string, object> {
            { "currency_pairs", string.Join(",", symbols) }
        };

        return _.SendRequestInternal<Dictionary<string, GateSpotUserTradingFee>>(_.GetUrl(api, version, section, batchFeeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List spot accounts
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotBalance>>> GetBalancesAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<List<GateSpotBalance>>(_.GetUrl(api, version, section, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: Query account book

    /// <summary>
    /// Create a batch of orders
    /// 
    /// Batch orders requirements:
    /// custom order field text is required
    /// At most 4 currency pairs, maximum 10 orders each, are allowed in one request
    /// No mixture of spot orders and margin orders, i.e. account must be identical for all orders
    /// </summary>
    /// <param name="requests">Request Objects</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<List<GateSpotBatchOrder>>> PlaceOrdersAsync(IEnumerable<GateSpotOrderRequest> requests, CancellationToken ct = default)
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

        return _.SendRequestInternal<List<GateSpotBatchOrder>>(_.GetUrl(api, version, section, batchOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List all open orders
    /// List open orders in all currency pairs.
    /// Note that pagination parameters affect record number in each currency pair's open order list. No pagination is applied to the number of currency pairs returned. All currency pairs with open orders will be returned.
    /// Spot,portfolio and margin orders are returned by default. To list cross margin orders, account must be set to cross_margin
    /// </summary>
    /// <param name="account"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotOpenOrders>>> GetOpenOrdersAsync(GateSpotAccountType? account = null, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalEnum("account", account);


        return _.SendRequestInternal<List<GateSpotOpenOrders>>(_.GetUrl(api, version, section, openOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Close position when cross-currency is disabled
    /// Currently, only cross-margin accounts are supported to close position when cross currencies are disabled. Maximum buy quantity = (unpaid principal and interest - currency balance - the amount of the currency in the order book) / 0.998
    /// </summary>
    /// <param name="request">Requests</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSpotOrder>> CloseLiquidatedPositionsAsync(GateSpotCloseRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new ParameterCollection()
        {
            { "currency_pair", request.Symbol },
        };
        parameters.AddString("price", request.Price);
        parameters.AddString("amount", request.Amount);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalEnum("action_mode", request.ProcessingMode);

        return _.SendRequestInternal<GateSpotOrder>(_.GetUrl(api, version, section, crossLiquidateOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Create an order
    /// 
    /// You can place orders with spot, portfolio, margin or cross margin account through setting the accountfield. It defaults to spot, which means spot account is used to place orders. If the user is using unified account, it defaults to the unified account.
    /// When margin account is used, i.e., account is margin, auto_borrow field can be set to true to enable the server to borrow the amount lacked using POST /margin/loans when your account's balance is not enough. Whether margin orders' fill will be used to repay margin loans automatically is determined by the auto repayment setting in your margin account, which can be updated or queried using /margin/auto_repay API.
    /// When cross margin account is used, i.e., account is cross_margin, auto_borrow can also be enabled to achieve borrowing the insufficient amount automatically if cross account's balance is not enough. But it differs from margin account that automatic repayment is determined by order's auto_repay field and only current order's fill will be used to repay cross margin loans.
    /// Automatic repayment will be triggered when the order is finished, i.e., its status is either cancelled or closed.
    /// 
    /// Order status
    /// An order waiting to be filled is open, and it stays open until it is filled totally. If fully filled, order is finished and its status turns to closed.If the order is cancelled before it is totally filled, whether or not partially filled, its status is cancelled. Iceberg order
    /// iceberg field can be used to set the amount shown. Set to -1 to hide the order completely. Note that the hidden part's fee will be charged using taker's fee rate. Self Trade Prevention
    /// Set stp_act to decide the strategy of self-trade prevention. For detailed usage, refer to the stp_act parameter in request body
    /// </summary>
    /// <param name="account">Account types， spot - spot account, margin - margin account, unified - unified account, cross_margin - cross margin account. Portfolio margin accounts can only be set to cross_margin</param>
    /// <param name="symbol">Currency pair</param>
    /// <param name="type">Order Type</param>
    /// <param name="side">Order side</param>
    /// <param name="timeInForce">Time in force</param>
    /// <param name="amount">When type is limit, it refers to base currency. For instance, BTC_USDT means BTC</param>
    /// <param name="price">Price can't be empty when type= limit</param>
    /// <param name="iceberg">Amount to display for the iceberg order. Null or 0 for normal orders. Hiding all amount is not supported.</param>
    /// <param name="autoBorrow">Used in margin or cross margin trading to allow automatic loan of insufficient amount if balance is not enough.</param>
    /// <param name="autoRepay">Enable or disable automatic repayment for automatic borrow loan generated by cross margin order. Default is disabled. Note that:</param>
    /// <param name="clientOrderId">User defined information. If not empty, must follow the rules below:</param>
    /// <param name="stpAction">Self-Trading Prevention Action. Users can use this field to set self-trade prevetion strategies</param>
    /// <param name="actionMode">Processing Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSpotOrder>> PlaceOrderAsync(
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
        string clientOrderId = null,
        GateSpotSelfTradeAction? stpAction = null,
        GateSpotActionMode? actionMode = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(new GateSpotOrderRequest
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
            SelfTradeAction = stpAction,
            ActionMode = actionMode,
        }, ct);
    
    /// <summary>
    /// Create an order
    /// 
    /// You can place orders with spot, portfolio, margin or cross margin account through setting the accountfield. It defaults to spot, which means spot account is used to place orders. If the user is using unified account, it defaults to the unified account.
    /// When margin account is used, i.e., account is margin, auto_borrow field can be set to true to enable the server to borrow the amount lacked using POST /margin/loans when your account's balance is not enough. Whether margin orders' fill will be used to repay margin loans automatically is determined by the auto repayment setting in your margin account, which can be updated or queried using /margin/auto_repay API.
    /// When cross margin account is used, i.e., account is cross_margin, auto_borrow can also be enabled to achieve borrowing the insufficient amount automatically if cross account's balance is not enough. But it differs from margin account that automatic repayment is determined by order's auto_repay field and only current order's fill will be used to repay cross margin loans.
    /// Automatic repayment will be triggered when the order is finished, i.e., its status is either cancelled or closed.
    /// 
    /// Order status
    /// An order waiting to be filled is open, and it stays open until it is filled totally. If fully filled, order is finished and its status turns to closed.If the order is cancelled before it is totally filled, whether or not partially filled, its status is cancelled. Iceberg order
    /// iceberg field can be used to set the amount shown. Set to -1 to hide the order completely. Note that the hidden part's fee will be charged using taker's fee rate. Self Trade Prevention
    /// Set stp_act to decide the strategy of self-trade prevention. For detailed usage, refer to the stp_act parameter in request body
    /// </summary>
    /// <param name="request">Order Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSpotOrder>> PlaceOrderAsync(GateSpotOrderRequest request, CancellationToken ct = default)
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
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddEnum("side", request.Side);
        parameters.AddEnum("type", request.Type);
        parameters.AddEnum("account", request.Account);
        parameters.AddEnum("time_in_force", request.TimeInForce);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalString("price", request.Price);
        parameters.AddOptional("iceberg", request.Iceberg);
        parameters.AddOptional("auto_borrow", request.AutoBorrow);
        parameters.AddOptional("auto_repay", request.AutoRepay);
        parameters.AddOptionalEnum("stp_act", request.SelfTradeAction);
        parameters.AddOptionalEnum("action_mode", request.ActionMode);

        return _.SendRequestInternal<GateSpotOrder>(_.GetUrl(api, version, section, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List orders
    /// Spot, portfolio and margin orders are returned by default. If cross margin orders are needed, account must be set to cross_margin
    /// When status is open, i.e., listing open orders, only pagination parameters page and limit are supported and limit cannot be larger than 100. Query by side and time range parameters from and to are not supported.
    /// When status is finished, i.e., listing finished orders, pagination parameters, time range parameters from and to, and side parameters are all supported. Time range parameters are handled as order finish time.
    /// </summary>
    /// <param name="symbol">Retrieve results with specified currency pair. It is required for open orders, but optional for finished ones.</param>
    /// <param name="status">List orders based on status</param>
    /// <param name="account">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned. If status is open, maximum of limit is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotOrder>>> GetOrdersAsync(
        string symbol,
        GateSpotOrderQueryStatus status,
        GateSpotAccountType account,
        GateSpotOrderSide side,
        DateTime from,
        DateTime to,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
        =>  GetOrdersAsync(symbol, status, account, side, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, ct);

    /// <summary>
    /// List orders
    /// Spot, portfolio and margin orders are returned by default. If cross margin orders are needed, account must be set to cross_margin
    /// When status is open, i.e., listing open orders, only pagination parameters page and limit are supported and limit cannot be larger than 100. Query by side and time range parameters from and to are not supported.
    /// When status is finished, i.e., listing finished orders, pagination parameters, time range parameters from and to, and side parameters are all supported. Time range parameters are handled as order finish time.
    /// </summary>
    /// <param name="symbol">Retrieve results with specified currency pair. It is required for open orders, but optional for finished ones.</param>
    /// <param name="status">List orders based on status</param>
    /// <param name="account">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned. If status is open, maximum of limit is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotOrder>>> GetOrdersAsync(
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

        return _.SendRequestInternal<List<GateSpotOrder>>(_.GetUrl(api, version, section, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel all open orders in specified currency pair
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="account">Specify account type:</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="actionMode">Processing Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotOrder>>> CancelOrdersAsync(
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

        return _.SendRequestInternal<List<GateSpotOrder>>(_.GetUrl(api, version, section, ordersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel a batch of orders with an ID list
    /// Multiple currency pairs can be specified, but maximum 20 orders are allowed per request
    /// </summary>
    /// <param name="requests">Request List</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotCancelOrder>>> CancelOrdersAsync(IEnumerable<GateSpotCancelOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
            SpotHelpers.ValidateMarketSymbol(request.Symbol);

        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        return _.SendRequestInternal<List<GateSpotCancelOrder>>(_.GetUrl(api, version, section, cancelBatchOrdersEndpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get a single order
    /// Spot, portfolio and margin orders are queried by default. If cross margin orders are needed or portfolio margin account are used, account must be set to cross_margin.
    /// </summary>
    /// <param name="symbol">Specify the transaction pair to query. If you are querying pending order records, this field is required. If you are querying traded records, this field can be left blank.</param>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="account">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSpotOrder>> GetOrderAsync(
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        GateSpotAccountType? account = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new ParameterCollection()
        {
            { "currency_pair", symbol },
        };
        parameters.AddOptionalEnum("account", account);

        var oid = _.CheckOrderId(orderId, clientOrderId);
        return _.SendRequestInternal<GateSpotOrder>(_.GetUrl(api, version, section, ordersEndpoint.AppendPath(oid)), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

#if NETSTANDARD2_1
    /// <summary>
    /// Amend an order
    /// By default, the orders of spot, portfolio and margin account are updated. If you need to modify orders of the cross-margin account, you must specify account as cross_margin. For portfolio margin account, only cross_margin account is supported.
    /// Currently, both request body and query support currency_pair and account parameter passing, but request body has higher priority
    /// Currently, only supports modification of price or amount fields.
    /// Regarding rate limiting: modify order and create order sharing rate limiting rules. Regarding matching priority: Only reducing the quantity without modifying the priority of matching, altering the price or increasing the quantity will adjust the priority to the new price at the end Note: If the modified amount is less than the fill amount, the order will be cancelled.
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="amount">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="price">New order price. amount and Price must specify one of them"</param>
    /// <param name="account">指定查询账户。</param>
    /// <param name="amendText">Custom info during amending order</param>
    /// <param name="actionMode">Processing Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSpotOrder>> AmendOrderAsync(
        string symbol = null,
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

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptionalString("amount", amount);
        parameters.AddOptionalString("price", price);
        parameters.AddOptional("amend_text", amendText);
        parameters.AddOptionalEnum("action_mode", actionMode);

        var oid = _.CheckOrderId(orderId, clientOrderId);
        var uri = _.GetUrl(api, version, section, ordersEndpoint.AppendPath(oid));
        if (!string.IsNullOrEmpty(symbol)) uri = uri.AddQueryParmeter("currency_pair", symbol);
        if (account != null) uri = uri.AddQueryParmeter("account", MapConverter.GetString(account));

        return _.SendRequestInternal<GateSpotOrder>(uri, HttpMethod.Patch, ct, true, bodyParameters: parameters);
    }
#endif

    /// <summary>
    /// Cancel a single order
    /// Spot,portfolio and margin orders are cancelled by default. If trying to cancel cross margin orders or portfolio margin account are used, account must be set to cross_margin
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="account">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="actionMode">Processing Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSpotOrder>> CancelOrderAsync(
        string symbol,
        long? orderId = null,
        string clientOrderId = null,
        GateSpotAccountType? account = null,
        GateSpotActionMode? actionMode = null,
        CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptionalEnum("account", account);
        parameters.AddOptionalEnum("action_mode", actionMode);

        var oid = _.CheckOrderId(orderId, clientOrderId);
        return _.SendRequestInternal<GateSpotOrder>(_.GetUrl(api, version, section, ordersEndpoint.AppendPath(oid)), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List personal trading history
    /// Spot,portfolio and margin trades are queried by default. If cross margin trades are needed, account must be set to cross_margin
    /// You can also set from and(or) to to query by time range. If you don't specify from and/or to parameters, only the last 7 days of data will be retured. The range of from and to is not alloed to exceed 30 days. Time range parameters are handled as order finish time.
    /// </summary>
    /// <param name="account">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="symbol">Retrieve results with specified currency pair</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list. Default: 100, Minimum: 1, Maximum: 1000</param>
    /// <param name="orderId">Filter trades with specified order ID. currency_pair is also required if this field is present</param>
    /// <param name="clientOrderId">Filter trades with specified order ID. currency_pair is also required if this field is present</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<List<GateSpotTradeHistory>>> GetTradeHistoryAsync(
    GateSpotAccountType account,
    string symbol,
    DateTime from,
    DateTime to,
    int page = 1,
    int limit = 100,
    long? orderId = null,
    string clientOrderId = null,
    CancellationToken ct = default)
        => GetTradeHistoryAsync(account, symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, orderId, clientOrderId, ct);

    /// <summary>
    /// List personal trading history
    /// Spot,portfolio and margin trades are queried by default. If cross margin trades are needed, account must be set to cross_margin
    /// You can also set from and(or) to to query by time range. If you don't specify from and/or to parameters, only the last 7 days of data will be retured. The range of from and to is not alloed to exceed 30 days. Time range parameters are handled as order finish time.
    /// </summary>
    /// <param name="account">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
    /// <param name="symbol">Retrieve results with specified currency pair</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list. Default: 100, Minimum: 1, Maximum: 1000</param>
    /// <param name="orderId">Filter trades with specified order ID. currency_pair is also required if this field is present</param>
    /// <param name="clientOrderId">Filter trades with specified order ID. currency_pair is also required if this field is present</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<List<GateSpotTradeHistory>>> GetTradeHistoryAsync(
        GateSpotAccountType? account = null,
        string symbol = null,
        long? from = null,
        long? to = null,
        int page = 1,
        int limit = 100,
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrEmpty(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var oid = _.CheckOrderId(orderId, clientOrderId);
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

        return _.SendRequestInternal<List<GateSpotTradeHistory>>(_.GetUrl(api, version, section, myTradesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get server current time
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await _.SendRequestInternal<GateSpotTime>(_.GetUrl(api, version, section, timeEndpoint), HttpMethod.Get, ct);
        return result.As(result.Data?.Time ?? default);
    }

    /// <summary>
    /// Countdown cancel orders
    /// When the timeout set by the user is reached, if there is no cancel or set a new countdown, the related pending orders will be automatically cancelled. This endpoint can be called repeatedly to set a new countdown or cancel the countdown. For example, call this endpoint at 30s intervals, each countdowntimeout is set to 30s. If this endpoint is not called again within 30 seconds, all pending orders on the specified market will be automatically cancelled, if no market is specified, all market pending orders will be cancelled. If the timeout is set to 0 within 30 seconds, the countdown timer will expire and the cacnel function will be cancelled.
    /// </summary>
    /// <param name="timeout">Countdown time, in seconds</param>
    /// <param name="symbol">Currency pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> CancelAllAsync(
        int timeout,
        string symbol = null,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new Dictionary<string, object> {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);
        var result = await _.SendRequestInternal<GateSpotCountdown>(_.GetUrl(api, version, section, countdownCancelAllEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Time ?? default);
    }

    // TODO: Batch modification of orders

    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="triggerPrice">Trigger price</param>
    /// <param name="triggerCondition">Price trigger condition</param>
    /// <param name="triggerExpiration">How long (in seconds) to wait for the condition to be triggered before cancelling the order.</param>
    /// <param name="orderAccount">Trading account type. Portfolio margin account must set to cross_margin</param>
    /// <param name="orderType">Order type，default to limit</param>
    /// <param name="orderSide">Order side</param>
    /// <param name="orderTimeInForce">time_in_force</param>
    /// <param name="orderAmount">When type is limit, it refers to base currency. For instance, BTC_USDT means BTC</param>
    /// <param name="orderPrice">Order price</param>
    /// <param name="orderClientOrderId">The source of the order, including:</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
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
        string orderClientOrderId = null,
        CancellationToken ct = default)
        => PlacePriceTriggeredOrderAsync(new GateSpotPriceTriggeredOrderRequest
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
        }, ct);

    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="request">Order Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateSpotPriceTriggeredOrderRequest request, CancellationToken ct = default)
    {
        SpotHelpers.ValidateMarketSymbol(request.Symbol);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var result = await _.SendRequestInternal<GateSpotPriceTriggeredOrderId>(_.GetUrl(api, version, section, priceOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }

    /// <summary>
    /// Retrieve running auto order list
    /// </summary>
    /// <param name="account">Trading account type. Portfolio margin account must set to cross_margin</param>
    /// <param name="status">Only list the orders with this status</param>
    /// <param name="symbol">Currency pair</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(
    GateSpotTriggerFilter status,
    GateSpotAccountType? account = null,
    string symbol = null,
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

        return _.SendRequestInternal<List<GateSpotPriceTriggeredOrder>>(_.GetUrl(api, version, section, priceOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel all open orders
    /// </summary>
    /// <param name="account">Trading account type. Portfolio margin account must set to cross_margin</param>
    /// <param name="symbol">Currency pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSpotPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(
        GateSpotAccountType? account = null,
        string symbol = null,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(symbol)) SpotHelpers.ValidateMarketSymbol(symbol);

        var parameters = new ParameterCollection();
        parameters.AddOptional("market", symbol);
        parameters.AddOptionalEnum("account", account);

        return _.SendRequestInternal<List<GateSpotPriceTriggeredOrder>>(_.GetUrl(api, version, section, priceOrdersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get a price-triggered order
    /// </summary>
    /// <param name="orderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="clientOrderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSpotPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var oid = _.CheckOrderId(orderId, clientOrderId);
        return _.SendRequestInternal<GateSpotPriceTriggeredOrder>(_.GetUrl(api, version, section, priceOrdersEndpoint.AppendPath(oid)), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Cancel a price-triggered order
    /// </summary>
    /// <param name="orderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="clientOrderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSpotPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var oid = _.CheckOrderId(orderId, clientOrderId);
        return _.SendRequestInternal<GateSpotPriceTriggeredOrder>(_.GetUrl(api, version, section, priceOrdersEndpoint.AppendPath(oid)), HttpMethod.Delete, ct, true);
    }
}