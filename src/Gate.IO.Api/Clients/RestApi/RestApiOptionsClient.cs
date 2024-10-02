using Gate.IO.Api.Models.RestApi.Options;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiOptionsClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string options = "options";

    // Endpoints
    private const string underlyingsEndpoint = "underlyings";
    private const string expirationsEndpoint = "expirations";
    private const string contractsEndpoint = "contracts";
    private const string settlementsEndpoint = "settlements";
    private const string mySettlementsEndpoint = "my_settlements";
    private const string orderBookEndpoint = "order_book";
    private const string tickersEndpoint = "tickers";
    private const string underlyingTickersUnderlyingEndpoint = "underlying/tickers/{underlying}";
    private const string candlesticksEndpoint = "candlesticks";
    private const string underlyingCandlesticksEndpoint = "underlying/candlesticks";
    private const string tradesEndpoint = "trades";
    private const string accountBookEndpoint = "account_book";
    private const string positionsEndpoint = "positions";
    private const string positionsContractEndpoint = "positions/{contract}";
    private const string positionCloseEndpoint = "position_close";
    private const string ordersEndpoint = "orders";
    private const string myTradesEndpoint = "my_trades";

    // Root Client
    internal GateRestApiClient Root { get; }

    internal RestApiOptionsClient(GateRestApiClient root)
    {
        Root = root;
    }

    #region List all underlyings
    public async Task<RestCallResult<IEnumerable<OptionsUnderlying>>> GetUnderlyingsAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<IEnumerable<OptionsUnderlying>>(Root.GetUrl(api, version, options, underlyingsEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region List all expiration times
    public async Task<RestCallResult<IEnumerable<long>>> GetExpirationsAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying }
        };

        //var jsonSerializer = new JsonSerializer();
        //jsonSerializer.Converters.Add(new DateTimeConverter());
        //return await Root.SendRequestInternal<IEnumerable<DateTime>>(RootClient.GetUrl(api, version, options, expirationsEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters, deserializer: jsonSerializer).ConfigureAwait(false);
        return await Root.SendRequestInternal<IEnumerable<long>>(Root.GetUrl(api, version, options, expirationsEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List all the contracts with specified underlying and expiration time
    public async Task<RestCallResult<IEnumerable<OptionsContract>>> GetContractsAsync(string underlying, long? expiration = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying }
        };
        parameters.AddOptionalParameter("expiration", expiration);

        return await Root.SendRequestInternal<IEnumerable<OptionsContract>>(Root.GetUrl(api, version, options, contractsEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query specified contract detail
    public async Task<RestCallResult<OptionsContract>> GetContractAsync(string contract, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<OptionsContract>(Root.GetUrl(api, version, options, contractsEndpoint.AppendPath(contract)), HttpMethod.Get, ct, false).ConfigureAwait(false);
    }
    #endregion

    #region List settlement history
    public async Task<RestCallResult<IEnumerable<OptionsSettlement>>> GetSettlementsAsync(string underlying, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetSettlementsAsync(underlying, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsSettlement>>> GetSettlementsAsync(string underlying, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsSettlement>>(Root.GetUrl(api, version, options, settlementsEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get specified contract's settlement
    public async Task<RestCallResult<OptionsSettlement>> GetSettlementAsync(string underlying, string contract, long at, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying },
            { "at", at },
        };

        return await Root.SendRequestInternal<OptionsSettlement>(Root.GetUrl(api, version, options, settlementsEndpoint.AppendPath(contract)), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List my options settlements
    public async Task<RestCallResult<IEnumerable<OptionsUserSettlement>>> GetUserSettlementsAsync(string underlying, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetUserSettlementsAsync(underlying, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsUserSettlement>>> GetUserSettlementsAsync(string underlying, string contract = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsUserSettlement>>(Root.GetUrl(api, version, options, mySettlementsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Options order book
    public async Task<RestCallResult<OptionsOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        return await Root.SendRequestInternal<OptionsOrderBook>(Root.GetUrl(api, version, options, orderBookEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List tickers of options contracts
    public async Task<RestCallResult<IEnumerable<OptionsContractTicker>>> GetContractTickersAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying },
        };

        return await Root.SendRequestInternal<IEnumerable<OptionsContractTicker>>(Root.GetUrl(api, version, options, tickersEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get underlying ticker
    public async Task<RestCallResult<OptionsUnderlyingTicker>> GetUnderlyingTickersAsync(string underlying, CancellationToken ct = default)
    {
        var endpoint = underlyingTickersUnderlyingEndpoint.Replace("{underlying}", underlying);
        return await Root.SendRequestInternal<OptionsUnderlyingTicker>(Root.GetUrl(api, version, options, endpoint), HttpMethod.Get, ct, false).ConfigureAwait(false);
    }
    #endregion

    #region Get options candlesticks
    public async Task<RestCallResult<IEnumerable<OptionsCandlestick>>> GetCandlesticksAsync(string contract, OptionsCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetCandlesticksAsync(contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsCandlestick>>> GetCandlesticksAsync(string contract, OptionsCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", JsonConvert.SerializeObject(interval, new OptionsCandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsCandlestick>>(Root.GetUrl(api, version, options, candlesticksEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Mark price candlesticks of an underlying
    public async Task<RestCallResult<IEnumerable<OptionsCandlestick>>> GetUnderlyingCandlesticksAsync(string underlying, OptionsCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetUnderlyingCandlesticksAsync(underlying, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsCandlestick>>> GetUnderlyingCandlesticksAsync(string underlying, OptionsCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying },
            { "interval", JsonConvert.SerializeObject(interval, new OptionsCandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsCandlestick>>(Root.GetUrl(api, version, options, underlyingCandlesticksEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Options trade history
    public async Task<RestCallResult<IEnumerable<OptionsTrade>>> GetTradesAsync(string contract, OptionsType type, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetTradesAsync(contract, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsTrade>>> GetTradesAsync(string contract, OptionsType type, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OptionsTypeConverter(false)));
        parameters.AddOptionalParameter("offset", offset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsTrade>>(Root.GetUrl(api, version, options, tradesEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List options account
    public async Task<RestCallResult<OptionsAccount>> GetAccountAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<OptionsAccount>(Root.GetUrl(api, version, options, tradesEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region List account changing history
    public async Task<RestCallResult<IEnumerable<OptionsAccountBook>>> GetAccountBookAsync(OptionsAccountBookType type, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetAccountBookAsync(type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsAccountBook>>> GetAccountBookAsync(OptionsAccountBookType type, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OptionsAccountBookTypeConverter(false)));
        parameters.AddOptionalParameter("offset", offset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsAccountBook>>(Root.GetUrl(api, version, options, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List user's positions of specified underlying
    public async Task<RestCallResult<IEnumerable<OptionsPosition>>> GetUnderlyingPositionsAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("underlying", underlying);

        return await Root.SendRequestInternal<IEnumerable<OptionsPosition>>(Root.GetUrl(api, version, options, positionsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get specified contract position
    public async Task<RestCallResult<IEnumerable<OptionsPosition>>> GetContractPositionsAsync(string contract, CancellationToken ct = default)
    {
        var endpoint = positionsContractEndpoint.Replace("{contract}", contract);
        return await Root.SendRequestInternal<IEnumerable<OptionsPosition>>(Root.GetUrl(api, version, options, endpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region List user's liquidation history of specified underlying
    public async Task<RestCallResult<IEnumerable<OptionsUserLiquidate>>> GetUserLiquidatesAsync(string underlying, string contract = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);

        return await Root.SendRequestInternal<IEnumerable<OptionsUserLiquidate>>(Root.GetUrl(api, version, options, positionCloseEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Create an order
    public async Task<RestCallResult<OptionsOrder>> PlaceOrderAsync(
        string contract,
        long size,
        long? iceberg = null,
        decimal? price = null,
        bool? close = null,
        bool? reduceOnly = null,
        OptionsTimeInForce? timeInForce= null,
        string clientOrderId = "",
        CancellationToken ct = default)
    {
        OptionsHelpers.ValidateContractSymbol(contract);
        ExchangeHelpers.ValidateClientOrderId(clientOrderId, true);

        var parameters = new Dictionary<string, object> {
            { "contract", contract },
            { "contract", size },
        };
        parameters.AddOptionalParameter("iceberg", iceberg);
        parameters.AddOptionalParameter("price", price?.ToGateString());
        parameters.AddOptionalParameter("close", close);
        parameters.AddOptionalParameter("reduce_only", reduceOnly);
        parameters.AddOptionalParameter("tif", JsonConvert.SerializeObject(timeInForce, new OptionsTimeInForceConverter(false)));
        parameters.AddOptionalParameter("text", clientOrderId);

        return await Root.SendRequestInternal<OptionsOrder>(Root.GetUrl(api, version, options, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List options orders
    public async Task<RestCallResult<IEnumerable<OptionsOrder>>> GetOrdersAsync(
    OptionsOrderStatus status,
    string underlying,
    string contract,
    DateTime from,
    DateTime to,
    int limit = 100,
    int offset = 0,
    CancellationToken ct = default)
        => await GetOrdersAsync(status, underlying, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsOrder>>> GetOrdersAsync(
        OptionsOrderStatus status,
        string underlying="",
        string contract="",
        long? from = null,
        long? to = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "status", JsonConvert.SerializeObject(status, new OptionsOrderStatusConverter(false)) },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsOrder>>(Root.GetUrl(api, version, options, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Cancel all open orders matched
    public async Task<RestCallResult<IEnumerable<OptionsOrder>>> CancelOrdersAsync(
        string underlying = "",
        string contract = "",
        OptionsOrderSide? side = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("side", JsonConvert.SerializeObject(side, new OptionsOrderSideConverter(false)));
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);

        return await Root.SendRequestInternal<IEnumerable<OptionsOrder>>(Root.GetUrl(api, version, options, ordersEndpoint), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get a single order
    public async Task<RestCallResult<OptionsOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<OptionsOrder>(Root.GetUrl(api, version, options, ordersEndpoint.AppendPath(orderId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Cancel a single order
    public async Task<RestCallResult<OptionsOrder>> CancelOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<OptionsOrder>(Root.GetUrl(api, version, options, ordersEndpoint.AppendPath(orderId.ToString())), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region List personal trading history
    public async Task<RestCallResult<IEnumerable<OptionsUserTrade>>> GetUserTradesAsync(
    string underlying,
    string contract,
    DateTime from,
    DateTime to,
    int limit = 100,
    int offset = 0,
    CancellationToken ct = default)
        => await GetUserTradesAsync(underlying, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<OptionsUserTrade>>> GetUserTradesAsync(
        string underlying,
        string contract = "",
        long? from = null,
        long? to = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "underlying", underlying },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<OptionsUserTrade>>(Root.GetUrl(api, version, options, myTradesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}