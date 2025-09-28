using System.Collections.Generic;
using System.Diagnostics.Contracts;

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

    /// <summary>
    /// List all underlyings
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOptionsUnderlying>>> GetUnderlyingsAsync(CancellationToken ct = default)
    {
        return await _.SendRequestInternal<List<GateOptionsUnderlying>>(_.GetUrl(api, v4, options, "underlyings"), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// List all expiration times
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<long>>> GetExpirationsAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying }
        };

        return await _.SendRequestInternal<List<long>>(_.GetUrl(api, v4, options, "expirations"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List all the contracts with specified underlying and expiration time
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="expiration">Unix timestamp of the expiration time</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOptionsContract>>> GetContractsAsync(string underlying, long? expiration = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying }
        };
        parameters.AddOptionalParameter("expiration", expiration);

        return await _.SendRequestInternal<List<GateOptionsContract>>(_.GetUrl(api, v4, options, "contracts"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query specified contract detail
    /// </summary>
    /// <param name="contract">Contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsContract>> GetContractAsync(string contract, CancellationToken ct = default)
    {
        return await _.SendRequestInternal<GateOptionsContract>(_.GetUrl(api, v4, options, "contracts".AppendPath(contract)), HttpMethod.Get, ct, false).ConfigureAwait(false);
    }

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
    public async Task<RestCallResult<List<GateOptionsSettlement>>> GetSettlementsAsync(
        string underlying,
        DateTime from,
        DateTime to,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    => await GetSettlementsAsync(underlying, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsSettlement>>> GetSettlementsAsync(
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
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsSettlement>>(_.GetUrl(api, v4, options, "settlements"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get specified contract's settlement
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="at">Timestamp</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsSettlement>> GetSettlementAsync(string underlying, string contract, long at, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
            { "at", at },
        };

        return await _.SendRequestInternal<GateOptionsSettlement>(_.GetUrl(api, v4, options, "settlements".AppendPath(contract)), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<GateOptionsUserSettlement>>> GetUserSettlementsAsync(string underlying, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetUserSettlementsAsync(underlying, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsUserSettlement>>> GetUserSettlementsAsync(string underlying, string contract = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsUserSettlement>>(_.GetUrl(api, v4, options, "my_settlements"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<GateOptionsOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        return await _.SendRequestInternal<GateOptionsOrderBook>(_.GetUrl(api, v4, options, "order_book"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List tickers of options contracts
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOptionsContractTicker>>> GetContractTickersAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };

        return await _.SendRequestInternal<List<GateOptionsContractTicker>>(_.GetUrl(api, v4, options, "tickers"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get underlying ticker
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsUnderlyingTicker>> GetUnderlyingTickersAsync(string underlying, CancellationToken ct = default)
    {
        var endpoint = "underlying/tickers/{underlying}".Replace("{underlying}", underlying);
        return await _.SendRequestInternal<GateOptionsUnderlyingTicker>(_.GetUrl(api, v4, options, endpoint), HttpMethod.Get, ct, false).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<GateOptionsCandlestick>>> GetCandlesticksAsync(string contract, GateOptionsCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetCandlesticksAsync(contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsCandlestick>>> GetCandlesticksAsync(string contract, GateOptionsCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("interval", interval);
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsCandlestick>>(_.GetUrl(api, v4, options, "candlesticks"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<GateOptionsCandlestickMark>>> GetUnderlyingCandlesticksAsync(string underlying, GateOptionsCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetUnderlyingCandlesticksAsync(underlying, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsCandlestickMark>>> GetUnderlyingCandlesticksAsync(string underlying, GateOptionsCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "underlying", underlying },
        };
        parameters.AddEnum("interval", interval);
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsCandlestickMark>>(_.GetUrl(api, v4, options, "underlying/candlesticks"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<GateOptionsTrade>>> GetTradesAsync(string contract, GateOptionsType type, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetTradesAsync(contract, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsTrade>>> GetTradesAsync(string contract, GateOptionsType type, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("offset", offset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsTrade>>(_.GetUrl(api, v4, options, "trades"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List options account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsBalance>> GetBalanceAsync(CancellationToken ct = default)
    {
        return await _.SendRequestInternal<GateOptionsBalance>(_.GetUrl(api, v4, options, "trades"), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Query account information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsAccount>> GetAccountAsync(CancellationToken ct = default)
    {
        return await _.SendRequestInternal<GateOptionsAccount>(_.GetUrl(api, v4, options, "accounts"), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

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
    public async Task<RestCallResult<List<GateOptionsBalanceChange>>> GetBalanceHistoryAsync(GateOptionsBalanceChangeType type, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetBalanceHistoryAsync(type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsBalanceChange>>> GetBalanceHistoryAsync(GateOptionsBalanceChangeType type, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("offset", offset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsBalanceChange>>(_.GetUrl(api, v4, options, "account_book"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List user's positions of specified underlying
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOptionsPosition>>> GetUnderlyingPositionsAsync(string underlying, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("underlying", underlying);

        return await _.SendRequestInternal<List<GateOptionsPosition>>(_.GetUrl(api, v4, options, "positions"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get specified contract position
    /// </summary>
    /// <param name="contract">Contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsPosition>> GetContractPositionAsync(string contract, CancellationToken ct = default)
    {
        var endpoint = "positions/{contract}".Replace("{contract}", contract);
        return await _.SendRequestInternal<GateOptionsPosition>(_.GetUrl(api, v4, options, endpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// List user's liquidation history of specified underlying
    /// </summary>
    /// <param name="underlying">Underlying (Obtained by listing underlying endpoint)</param>
    /// <param name="contract">Contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOptionsUserLiquidation>>> GetUserLiquidationsAsync(string underlying, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);

        return await _.SendRequestInternal<List<GateOptionsUserLiquidation>>(_.GetUrl(api, v4, options, "position_close"), HttpMethod.Get, ct, true).ConfigureAwait(false);
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
    /// <param name="mmp">设置为 true 的时候，为MMP委托</param>
    /// <param name="timeInForce">Time in force</param>
    /// <param name="clientOrderId">User defined information. If not empty, must follow the rules below:</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsOrder>> PlaceOrderAsync(
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
    {
        OptionsHelpers.ValidateContractSymbol(contract);
        ExchangeHelpers.ValidateClientOrderId(clientOrderId, true);

        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "size", size },
        };
        parameters.AddOptional("iceberg", iceberg);
        parameters.AddOptionalString("price", price);
        parameters.AddOptional("close", close);
        parameters.AddOptional("reduce_only", reduceOnly);
        parameters.AddOptional("mmp", mmp);
        parameters.AddOptionalEnum("tif", timeInForce);
        parameters.AddOptional("text", clientOrderId);

        return await _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<GateOptionsOrder>>> GetOrdersAsync(
    GateOptionsOrderStatus status,
    string underlying,
    string contract,
    DateTime from,
    DateTime to,
    int limit = 100,
    int offset = 0,
    CancellationToken ct = default)
        => await GetOrdersAsync(status, underlying, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

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
    public async Task<RestCallResult<List<GateOptionsOrder>>> GetOrdersAsync(
        GateOptionsOrderStatus status,
        string underlying = null,
        string contract = null,
        long? from = null,
        long? to = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddEnum("status", status);
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await _.SendRequestInternal<List<GateOptionsOrder>>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel all open orders matched
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateOptionsOrder>>> CancelOrdersAsync(
        string underlying = null,
        string contract = null,
        GateOptionsOrderSide? side = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("side", side);
        parameters.AddOptionalParameter("underlying", underlying);
        parameters.AddOptionalParameter("contract", contract);

        return await _.SendRequestInternal<List<GateOptionsOrder>>(_.GetUrl(api, v4, options, "orders"), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders".AppendPath(orderId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel a single order
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<GateOptionsOrder>> CancelOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await _.SendRequestInternal<GateOptionsOrder>(_.GetUrl(api, v4, options, "orders".AppendPath(orderId.ToString())), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Countdown cancel orders
    /// Option order heartbeat detection, when the timeout time set by the user is reached, if the existing countdown is not canceled or a new countdown is set, the related option pending order will be automatically canceled.This interface can be called repeatedly to set a new countdown or cancel the countdown.Usage example: Repeat this interface at intervals of 30 seconds, with each countdown timeout set to 30 (seconds). If this interface is not called again within 30 seconds, all pending orders on the underlying contract you specified will be automatically cancelled.If underlying contract is not specified, user will be automatically cancelled If timeout is set to 0 within 30 seconds, the countdown timer will expire and the automatic order cancellation function will be cancelled.
    /// </summary>
    /// <param name="timeout">Countdown time in seconds</param>
    /// <param name="contract">Options contract name</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> CancelAllAsync(int timeout, string contract = null, string underlying = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("underlying", underlying);
        var result = await _.SendRequestInternal<GateOptionsCountdown>(_.GetUrl(api, v4, options, "countdown_cancel_all"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    public async Task<RestCallResult<List<GateOptionsUserTrade>>> GetUserTradesAsync(
        string underlying,
        string contract,
        DateTime from,
        DateTime to,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetUserTradesAsync(underlying, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

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
    public  Task<RestCallResult<List<GateOptionsUserTrade>>> GetUserTradesAsync(
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
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return  _.SendRequestInternal<List<GateOptionsUserTrade>>(_.GetUrl(api, v4, options, "my_trades"), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    {
        var parameters = new ParameterCollection {
            { "underlying", underlying },
            { "window", window },
            { "frozen_period", frozenPeriod },
        };
        parameters.AddString("qty_limit", quantityLimit);
        parameters.AddString("delta_limit", deltaLimit);

        return _.SendRequestInternal<GateOptionsMMP>(_.GetUrl(api, v4, options, "mmp"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// MMP Query.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateOptionsMMP>>> GetMMPAsync(string underlying=null, CancellationToken ct = default)
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