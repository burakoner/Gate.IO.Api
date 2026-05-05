namespace Gate.IO.Api.Options;

/// <summary>
/// Gate.IO Options REST API Client
/// </summary>
public class GateOptionsRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string options = "options";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateOptionsRestApiClient(GateRestApiClient root) => _ = root;

    private static void AddPaging(ParameterCollection parameters, int? limit, int? offset)
    {
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);
    }

    private static void AddRawTimeRange(ParameterCollection parameters, long? from, long? to)
    {
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
    }

    private static void AddTimeRange(ParameterCollection parameters, DateTime? from, DateTime? to)
    {
        parameters.AddOptionalSeconds("from", from);
        parameters.AddOptionalSeconds("to", to);
    }

    /// <summary>
    /// List all underlyings
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUnderlying>>> GetUnderlyingsAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateOptionsUnderlying>>(_.GetUrl(api, v4, options, "underlyings"), HttpMethod.Get, ct);

    /// <summary>
    /// List all expiration times
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<long>>> GetExpirationsAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying }
        };

        return _.SendRequestInternal<List<long>>(_.GetUrl(api, v4, options, "expirations"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// List all the contracts with specified underlying and expiration time
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="expiration">Unix timestamp of the expiration time</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsContract>>> GetContractsAsync(string underlying, long? expiration = null, CancellationToken ct = default)
        => GetContractsAsync(new GateOptionsContractQueryRequest { Underlying = underlying, Expiration = expiration }, ct);

    /// <summary>
    /// List all the contracts with specified underlying and expiration time
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsContract>>> GetContractsAsync(GateOptionsContractQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", request.Underlying }
        };
        parameters.AddOptionalParameter("expiration", request.Expiration);

        return _.SendRequestInternal<List<GateOptionsContract>>(_.GetUrl(api, v4, options, "contracts"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Query specified contract detail
    /// </summary>
    /// <param name="contract">Contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => _.SendRequestInternal<GateOptionsContract>(_.GetUrl(api, v4, options, "contracts".AppendPath(contract)), HttpMethod.Get, ct, false);

    /// <summary>
    /// List settlement history
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">End timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsSettlement>>> GetSettlementsAsync(
        string underlying,
        DateTime from,
        DateTime to,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetSettlementsAsync(new GateOptionsSettlementQueryRequest
        {
            Underlying = underlying,
            From = from,
            To = to,
            Limit = limit,
            Offset = offset,
        }, ct);

    /// <summary>
    /// List settlement history
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">End timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsSettlement>>> GetSettlementsAsync(
        string underlying,
        long? from = null,
        long? to = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };
        AddPaging(parameters, limit, offset);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsSettlement>>(_.GetUrl(api, v4, options, "settlements"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// List settlement history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsSettlement>>> GetSettlementsAsync(GateOptionsSettlementQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", request.Underlying },
        };
        AddPaging(parameters, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsSettlement>>(_.GetUrl(api, v4, options, "settlements"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Get specified contract's settlement
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="at">Timestamp</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsSettlement>> GetSettlementAsync(string underlying, string contract, long at, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
            { "at", at },
        };

        return _.SendRequestInternal<GateOptionsSettlement>(_.GetUrl(api, v4, options, "settlements".AppendPath(contract)), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// List my options settlements
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">End timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserSettlement>>> GetUserSettlementsAsync(string underlying, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetUserSettlementsAsync(new GateOptionsUserSettlementQueryRequest
        {
            Underlying = underlying,
            Contract = contract,
            From = from,
            To = to,
            Limit = limit,
            Offset = offset,
        }, ct);

    /// <summary>
    /// List my options settlements
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">End timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserSettlement>>> GetUserSettlementsAsync(string underlying, string contract = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };
        parameters.AddOptionalParameter("contract", contract);
        AddPaging(parameters, limit, offset);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsUserSettlement>>(_.GetUrl(api, v4, options, "my_settlements"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List my options settlements
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserSettlement>>> GetUserSettlementsAsync(GateOptionsUserSettlementQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", request.Underlying },
        };
        parameters.AddOptionalParameter("contract", request.Contract);
        AddPaging(parameters, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsUserSettlement>>(_.GetUrl(api, v4, options, "my_settlements"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Options order book
    /// </summary>
    /// <param name="contract">Options contract name</param>
    /// <param name="interval">Order depth. 0 means no aggregation is applied. default to 0</param>
    /// <param name="limit">Maximum number of order depth data in asks or bids</param>
    /// <param name="withId">Whether the order book update ID will be returned. This ID increases by 1 on every order book update</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => GetOrderBookAsync(new GateOptionsOrderBookRequest { Contract = contract, Interval = interval, Limit = limit, WithId = withId }, ct);

    /// <summary>
    /// Options order book
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrderBook>> GetOrderBookAsync(GateOptionsOrderBookRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", request.Contract },
        };
        parameters.AddOptionalString("interval", request.Interval);
        parameters.AddOptional("limit", request.Limit);
        parameters.AddOptional("with_id", request.WithId?.ToString().ToLower());

        return _.SendRequestInternal<GateOptionsOrderBook>(_.GetUrl(api, v4, options, "order_book"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// List tickers of options contracts
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsContractTicker>>> GetContractTickersAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };

        return _.SendRequestInternal<List<GateOptionsContractTicker>>(_.GetUrl(api, v4, options, "tickers"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Get underlying ticker
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsUnderlyingTicker>> GetUnderlyingTickersAsync(string underlying, CancellationToken ct = default)
    {
        var endpoint = "underlying/tickers/{underlying}".Replace("{underlying}", underlying);
        return _.SendRequestInternal<GateOptionsUnderlyingTicker>(_.GetUrl(api, v4, options, endpoint), HttpMethod.Get, ct, false);
    }

    /// <summary>
    /// Get options candlesticks
    /// </summary>
    /// <param name="contract">Options contract name</param>
    /// <param name="interval">Interval time between data points</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">To timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsCandlestick>>> GetCandlesticksAsync(string contract, GateOptionsCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => GetCandlesticksAsync(new GateOptionsCandlestickQueryRequest { Contract = contract, Interval = interval, From = from, To = to, Limit = limit }, ct);

    /// <summary>
    /// Get options candlesticks
    /// </summary>
    /// <param name="contract">Options contract name</param>
    /// <param name="interval">Interval time between data points</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">To timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsCandlestick>>> GetCandlesticksAsync(string contract, GateOptionsCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("interval", interval);
        AddPaging(parameters, limit, null);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsCandlestick>>(_.GetUrl(api, v4, options, "candlesticks"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Get options candlesticks
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsCandlestick>>> GetCandlesticksAsync(GateOptionsCandlestickQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", request.Contract },
        };
        parameters.AddOptionalEnum("interval", request.Interval);
        AddPaging(parameters, request.Limit, null);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsCandlestick>>(_.GetUrl(api, v4, options, "candlesticks"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Mark price candlesticks of an underlying
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="interval">Interval time between data points</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">To timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsCandlestickMark>>> GetUnderlyingCandlesticksAsync(string underlying, GateOptionsCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => GetUnderlyingCandlesticksAsync(new GateOptionsUnderlyingCandlestickQueryRequest { Underlying = underlying, Interval = interval, From = from, To = to, Limit = limit }, ct);

    /// <summary>
    /// Mark price candlesticks of an underlying
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="interval">Interval time between data points</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">To timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsCandlestickMark>>> GetUnderlyingCandlesticksAsync(string underlying, GateOptionsCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };
        parameters.AddEnum("interval", interval);
        AddPaging(parameters, limit, null);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsCandlestickMark>>(_.GetUrl(api, v4, options, "underlying/candlesticks"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Mark price candlesticks of an underlying
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsCandlestickMark>>> GetUnderlyingCandlesticksAsync(GateOptionsUnderlyingCandlestickQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", request.Underlying },
        };
        parameters.AddOptionalEnum("interval", request.Interval);
        AddPaging(parameters, request.Limit, null);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsCandlestickMark>>(_.GetUrl(api, v4, options, "underlying/candlesticks"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Options trade history
    /// </summary>
    /// <param name="contract">Options contract name</param>
    /// <param name="type">C is call, while P is put</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">To timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsTrade>>> GetTradesAsync(string contract, GateOptionsType type, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetTradesAsync(new GateOptionsTradeQueryRequest { Contract = contract, Type = type, From = from, To = to, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Options trade history
    /// </summary>
    /// <param name="contract">Options contract name</param>
    /// <param name="type">C is call, while P is put</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">To timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsTrade>>> GetTradesAsync(string contract, GateOptionsType type, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalEnum("type", type);
        AddPaging(parameters, limit, offset);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsTrade>>(_.GetUrl(api, v4, options, "trades"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Options trade history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsTrade>>> GetTradesAsync(GateOptionsTradeQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", request.Contract);
        parameters.AddOptionalEnum("type", request.Type);
        AddPaging(parameters, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsTrade>>(_.GetUrl(api, v4, options, "trades"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// List options account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsBalance>> GetBalanceAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateOptionsBalance>(_.GetUrl(api, v4, options, "accounts"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query account information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsAccount>> GetAccountAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateOptionsAccount>(_.GetUrl(api, v4, options, "accounts"), HttpMethod.Get, ct, true);

    /// <summary>
    /// List account changing history
    /// </summary>
    /// <param name="type">Changing Type:</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">End timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsBalanceChange>>> GetBalanceHistoryAsync(GateOptionsBalanceChangeType type, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetBalanceHistoryAsync(new GateOptionsBalanceHistoryQueryRequest { Type = type, From = from, To = to, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// List account changing history
    /// </summary>
    /// <param name="type">Changing Type:</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">End timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsBalanceChange>>> GetBalanceHistoryAsync(GateOptionsBalanceChangeType type, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        AddPaging(parameters, limit, offset);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsBalanceChange>>(_.GetUrl(api, v4, options, "account_book"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List account changing history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsBalanceChange>>> GetBalanceHistoryAsync(GateOptionsBalanceHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", request.Type);
        AddPaging(parameters, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsBalanceChange>>(_.GetUrl(api, v4, options, "account_book"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List user's positions of specified underlying
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsPosition>>> GetUnderlyingPositionsAsync(string underlying, CancellationToken ct = default)
        => GetUnderlyingPositionsAsync(new GateOptionsPositionQueryRequest { Underlying = underlying }, ct);

    /// <summary>
    /// List user's positions of specified underlying
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsPosition>>> GetUnderlyingPositionsAsync(GateOptionsPositionQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("underlying", request.Underlying);

        return _.SendRequestInternal<List<GateOptionsPosition>>(_.GetUrl(api, v4, options, "positions"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get specified contract position
    /// </summary>
    /// <param name="contract">Contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsPosition>> GetContractPositionAsync(string contract, CancellationToken ct = default)
    {
        var endpoint = "positions/{contract}".Replace("{contract}", contract);
        return _.SendRequestInternal<GateOptionsPosition>(_.GetUrl(api, v4, options, endpoint), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// List user's liquidation history of specified underlying
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserLiquidation>>> GetUserLiquidationsAsync(string underlying, string contract = null, CancellationToken ct = default)
        => GetUserLiquidationsAsync(new GateOptionsUserLiquidationQueryRequest { Underlying = underlying, Contract = contract }, ct);

    /// <summary>
    /// List user's liquidation history of specified underlying
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserLiquidation>>> GetUserLiquidationsAsync(GateOptionsUserLiquidationQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", request.Underlying },
        };
        parameters.AddOptionalParameter("contract", request.Contract);

        return _.SendRequestInternal<List<GateOptionsUserLiquidation>>(_.GetUrl(api, v4, options, "position_close"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="contract">Contract name</param>
    /// <param name="size">Order size. Specify positive number to make a bid, and negative number to ask</param>
    /// <param name="iceberg">Display size for iceberg order. 0 for non-iceberg. Note that you will have to pay the taker fee for the hidden size</param>
    /// <param name="price">Order price. 0 for market order with tif set as ioc (USDT)</param>
    /// <param name="close">Set as true to close the position, with size set to 0</param>
    /// <param name="reduceOnly">Set as true to be reduce-only order</param>
    /// <param name="mmp">Set as true to create an MMP order</param>
    /// <param name="timeInForce">Time in force</param>
    /// <param name="clientOrderId">User defined information</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> PlaceOrderAsync(
        string contract,
        long size,
        long? iceberg = null,
        decimal? price = null,
        bool? close = null,
        bool? reduceOnly = null,
        bool? mmp = null,
        GateOptionsTimeInForce? timeInForce = null,
        string clientOrderId = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(new GateOptionsOrderRequest
        {
            Contract = contract,
            Size = size,
            Iceberg = iceberg,
            Price = price,
            Close = close,
            ReduceOnly = reduceOnly,
            Mmp = mmp,
            TimeInForce = timeInForce,
            ClientOrderId = clientOrderId,
        }, ct);

    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> PlaceOrderAsync(GateOptionsOrderRequest request, CancellationToken ct = default)
    {
        OptionsHelpers.ValidateContractSymbol(request.Contract);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new ParameterCollection
        {
            { "contract", request.Contract },
            { "size", request.Size },
        };
        parameters.AddOptional("iceberg", request.Iceberg);
        parameters.AddOptionalString("price", request.Price);
        parameters.AddOptional("close", request.Close);
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptional("mmp", request.Mmp);
        parameters.AddOptionalEnum("tif", request.TimeInForce);
        parameters.AddOptional("text", request.ClientOrderId);

        return _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List options orders
    /// </summary>
    /// <param name="status">Only list the orders with this status</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">End timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsOrder>>> GetOrdersAsync(
        GateOptionsOrderStatus status,
        string underlying,
        string contract,
        DateTime from,
        DateTime to,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetOrdersAsync(new GateOptionsOrderQueryRequest { Status = status, Underlying = underlying, Contract = contract, From = from, To = to, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// List options orders
    /// </summary>
    /// <param name="status">Only list the orders with this status</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">End timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsOrder>>> GetOrdersAsync(
        GateOptionsOrderStatus status,
        string underlying = null,
        string contract = null,
        long? from = null,
        long? to = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("status", status);
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);
        AddPaging(parameters, limit, offset);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsOrder>>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List options orders
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsOrder>>> GetOrdersAsync(GateOptionsOrderQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("status", request.Status);
        parameters.AddOptionalParameter("underlying", request.Underlying);
        parameters.AddOptionalParameter("contract", request.Contract);
        AddPaging(parameters, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsOrder>>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel all open orders matched
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsOrder>>> CancelOrdersAsync(
        string underlying = null,
        string contract = null,
        GateOptionsOrderSide? side = null,
        CancellationToken ct = default)
        => CancelOrdersAsync(new GateOptionsCancelOrdersRequest { Underlying = underlying, Contract = contract, Side = side }, ct);

    /// <summary>
    /// Cancel all open orders matched
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsOrder>>> CancelOrdersAsync(GateOptionsCancelOrdersRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("side", request.Side);
        parameters.AddOptionalParameter("underlying", request.Underlying);
        parameters.AddOptionalParameter("contract", request.Contract);

        return _.SendRequestInternal<List<GateOptionsOrder>>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        => _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders".AppendPath(orderId.ToString())), HttpMethod.Get, ct, true);

    /// <summary>
    /// Amend a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="price">Order price</param>
    /// <param name="size">Trade amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> AmendOrderAsync(long orderId, string contract, decimal price, long size, CancellationToken ct = default)
        => AmendOrderAsync(orderId, new GateOptionsOrderUpdateRequest { Contract = contract, Price = price, Size = size }, ct);

    /// <summary>
    /// Amend a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> AmendOrderAsync(long orderId, GateOptionsOrderUpdateRequest request, CancellationToken ct = default)
    {
        OptionsHelpers.ValidateContractSymbol(request.Contract);

        var parameters = new ParameterCollection
        {
            { "contract", request.Contract },
            { "size", request.Size },
        };
        parameters.AddString("price", request.Price);

        return _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders".AppendPath(orderId.ToString())), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Update a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="price">Order price</param>
    /// <param name="size">Trade amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> UpdateOrderAsync(long orderId, string contract, decimal price, long size, CancellationToken ct = default)
        => AmendOrderAsync(orderId, contract, price, size, ct);

    /// <summary>
    /// Update a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> UpdateOrderAsync(long orderId, GateOptionsOrderUpdateRequest request, CancellationToken ct = default)
        => AmendOrderAsync(orderId, request, ct);

    /// <summary>
    /// Cancel a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsOrder>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        => _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders".AppendPath(orderId.ToString())), HttpMethod.Delete, ct, true);

    /// <summary>
    /// Countdown cancel orders
    /// </summary>
    /// <param name="timeout">Countdown time in seconds</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<DateTime>> CancelAllAsync(int timeout, string contract = null, string underlying = null, CancellationToken ct = default)
        => CancelAllAsync(new GateOptionsCountdownCancelAllRequest { Timeout = timeout, Contract = contract, Underlying = underlying }, ct);

    /// <summary>
    /// Countdown cancel orders
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> CancelAllAsync(GateOptionsCountdownCancelAllRequest request, CancellationToken ct = default)
    {
        if (request.Timeout != 0 && request.Timeout < 5)
            throw new ArgumentException("Timeout must be 0 or at least 5 seconds", nameof(request.Timeout));

        var parameters = new ParameterCollection {
            { "timeout", request.Timeout },
        };
        parameters.AddOptionalParameter("contract", request.Contract);
        parameters.AddOptionalParameter("underlying", request.Underlying);

        var result = await _.SendRequestInternal<GateOptionsCountdown>(_.GetUrl(api, v4, options, "countdown_cancel_all"), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
        return result.As(result.Data?.Time ?? default);
    }

    /// <summary>
    /// List personal trading history
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="from">Start timestamp</param>
    /// <param name="to">End timestamp</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserTrade>>> GetUserTradesAsync(
        string underlying,
        string contract,
        DateTime from,
        DateTime to,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetUserTradesAsync(new GateOptionsUserTradeQueryRequest { Underlying = underlying, Contract = contract, From = from, To = to, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// List personal trading history
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="from">Start timestamp in Unix seconds</param>
    /// <param name="to">End timestamp in Unix seconds</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserTrade>>> GetUserTradesAsync(
        string underlying,
        string contract = null,
        long? from = null,
        long? to = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };
        parameters.AddOptionalParameter("contract", contract);
        AddPaging(parameters, limit, offset);
        AddRawTimeRange(parameters, from, to);

        return _.SendRequestInternal<List<GateOptionsUserTrade>>(_.GetUrl(api, v4, options, "my_trades"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List personal trading history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsUserTrade>>> GetUserTradesAsync(GateOptionsUserTradeQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", request.Underlying },
        };
        parameters.AddOptionalParameter("contract", request.Contract);
        AddPaging(parameters, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateOptionsUserTrade>>(_.GetUrl(api, v4, options, "my_trades"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// MMP Settings
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="window">Time window (milliseconds), between 1-5000, 0 means disable MMP</param>
    /// <param name="frozenPeriod">Freeze duration (milliseconds), 0 means always frozen, need to call reset API to unfreeze</param>
    /// <param name="quantityLimit">Trading volume upper limit (positive number, up to 2 decimal places)</param>
    /// <param name="deltaLimit">Upper limit of net delta value (positive number, up to 2 decimal places)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsMMP>> SetMMPAsync(string underlying, int window, int frozenPeriod, decimal quantityLimit, decimal deltaLimit, CancellationToken ct = default)
        => SetMMPAsync(new GateOptionsMMPRequest { Underlying = underlying, Window = window, FrozenPeriod = frozenPeriod, QuantityLimit = quantityLimit, DeltaLimit = deltaLimit }, ct);

    /// <summary>
    /// MMP Settings
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsMMP>> SetMMPAsync(GateOptionsMMPRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "underlying", request.Underlying },
            { "window", request.Window },
            { "frozen_period", request.FrozenPeriod },
        };
        parameters.AddString("qty_limit", request.QuantityLimit);
        parameters.AddString("delta_limit", request.DeltaLimit);

        return _.SendRequestInternal<GateOptionsMMP>(_.GetUrl(api, v4, options, "mmp"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// MMP Query.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsMMP>>> GetMMPAsync(string underlying = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("underlying", underlying);

        return _.SendRequestInternal<List<GateOptionsMMP>>(_.GetUrl(api, v4, options, "mmp"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// MMP Reset
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateOptionsMMP>> ResetMMPAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "underlying", underlying },
        };

        return _.SendRequestInternal<GateOptionsMMP>(_.GetUrl(api, v4, options, "mmp/reset"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
}
