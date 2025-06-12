namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Perpetual Settlement REST API Client
/// </summary>
public class GateFuturesRestApiSettleClient
{
    internal GateFuturesSettlement Settlement { get; }
    internal GateFuturesRestApiClient _ { get; }

    internal GateFuturesRestApiSettleClient(GateFuturesRestApiClient main, GateFuturesSettlement settle)
    {
        _ = main;
        Settlement = settle;
    }

    /// <summary>
    /// List all futures contracts
    /// </summary>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesContract>>> GetContractsAsync(int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetContractsAsync(Settlement, limit, offset, ct);

    /// <summary>
    /// Get a single contract
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => _.GetContractAsync(Settlement, contract, ct);

    /// <summary>
    /// Futures order book
    /// Bids will be sorted by price from high to low, while asks sorted reversely
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Order depth. 0 means no aggregation is applied. default to 0</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="withId">Whether the order book update ID will be returned. This ID increases by 1 on every order book update</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => _.GetOrderBookAsync(Settlement, contract, interval, limit, withId, ct);

    /// <summary>
    /// Futures trading history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="lastId">Specify the starting point for this list based on a previously retrieved id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    /// <summary>
    /// Futures trading history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="lastId">Specify the starting point for this list based on a previously retrieved id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    /// <summary>
    /// Get futures candlesticks
    /// Return specified contract candlesticks. If prefix contract with mark_, the contract's mark price candlesticks are returned; if prefix with index_, index price candlesticks will be returned.
    /// Maximum of 2000 points are returned in one query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval time between data points. Note that 1w means natual week(Mon-Sun), while 7d means every 7d since unix 0. Note that 30d means 1 natual month, not 30 days</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    /// <summary>
    /// Get futures candlesticks
    /// Return specified contract candlesticks. If prefix contract with mark_, the contract's mark price candlesticks are returned; if prefix with index_, index price candlesticks will be returned.
    /// Maximum of 2000 points are returned in one query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval time between data points. Note that 1w means natual week(Mon-Sun), while 7d means every 7d since unix 0. Note that 30d means 1 natual month, not 30 days</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);
    
    /// <summary>
    /// Get futures candlesticks
    /// Return specified contract candlesticks. If prefix contract with mark_, the contract's mark price candlesticks are returned; if prefix with index_, index price candlesticks will be returned.
    /// Maximum of 2000 points are returned in one query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval time between data points. Note that 1w means natual week(Mon-Sun), while 7d means every 7d since unix 0. Note that 30d means 1 natual month, not 30 days</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);
    
    /// <summary>
    /// Get futures candlesticks
    /// Return specified contract candlesticks. If prefix contract with mark_, the contract's mark price candlesticks are returned; if prefix with index_, index price candlesticks will be returned.
    /// Maximum of 2000 points are returned in one query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval time between data points. Note that 1w means natual week(Mon-Sun), while 7d means every 7d since unix 0. Note that 30d means 1 natual month, not 30 days</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    /// <summary>
    /// Premium Index K-Line
    /// Maximum of 1000 points can be returned in a query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval time between data points</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);
    
    /// <summary>
    /// Premium Index K-Line
    /// Maximum of 1000 points can be returned in a query. Be sure not to exceed the limit when specifying from, to and interval
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval time between data points</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    /// <summary>
    /// List futures tickers
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(string contract = null, CancellationToken ct = default)
        => _.GetTickersAsync(Settlement, contract, ct);

    /// <summary>
    /// Funding rate history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetFundingRateHistoryAsync(Settlement, contract, from, to, limit, ct);

    /// <summary>
    /// Futures insurance balance history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetFundingRateHistoryAsync(Settlement, contract, from, to, limit, ct);

    /// <summary>
    /// Futures insurance balance history
    /// </summary>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => _.GetInsuranceHistoryAsync(Settlement, limit, ct);

    /// <summary>
    /// Futures stats
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(string contract, GateFuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
        => _.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    /// <summary>
    /// Futures stats
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="interval">Interval</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(string contract, GateFuturesStatsInterval? interval = null, long? from = null, int limit = 100, CancellationToken ct = default)
        => _.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    /// <summary>
    /// Get index constituents
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesIndexConstituents>> GetIndexConstituentsAsync(string index, CancellationToken ct = default)
        => _.GetIndexConstituentsAsync(Settlement, index, ct);

    /// <summary>
    /// Retrieve liquidation history.
    /// Interval between from and to cannot exceeds 3600. Some private fields will not be returned in public endpoints. Refer to field description for detail.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetLiquidationsAsync(Settlement, contract, from, to, limit, ct);

    /// <summary>
    /// Retrieve liquidation history.
    /// Interval between from and to cannot exceeds 3600. Some private fields will not be returned in public endpoints. Refer to field description for detail.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetLiquidationsAsync(Settlement, contract, from, to, limit, ct);

    /// <summary>
    /// When the 'contract' parameter is not passed, the default is to query the risk limits for the top 100 markets.'Limit' and 'offset' correspond to pagination queries at the market level, not to the length of the returned array. This only takes effect when the 'contract' parameter is empty.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesRiskLimitTier>>> GetRiskLimitTiersAsync(string contract = null, int limit = 100, long? offset = null, CancellationToken ct = default)
        => _.GetRiskLimitTiersAsync(Settlement, contract, limit, offset, ct);

    /// <summary>
    /// Query futures account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesBalance>> GetBalancesAsync(CancellationToken ct = default)
        => _.GetBalancesAsync(Settlement, ct);

    /// <summary>
    /// Query account book
    /// If the contract field is provided, it can only filter records that include this field after 2023-10-30.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="type">Changing Type</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(string contract, DateTime from, DateTime to, GateFuturesBalanceChangeType type, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetBalanceHistoryAsync(Settlement, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, limit, offset, ct);
    
    /// <summary>
    /// Query account book
    /// If the contract field is provided, it can only filter records that include this field after 2023-10-30.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="type">Changing Type</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(string contract = null, long? from = null, long? to = null, GateFuturesBalanceChangeType? type = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetBalanceHistoryAsync(Settlement, contract, from, to, type, limit, offset, ct);

    /// <summary>
    /// List all positions of a user
    /// </summary>
    /// <param name="holding">Return only real positions - true, return all - false.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(bool? holding = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPositionsAsync(Settlement, holding, limit, offset, ct);

    /// <summary>
    /// Get single position
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => _.GetPositionAsync(Settlement, contract, ct);

    /// <summary>
    /// Update position margin
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="change">Margin change. Use positive number to increase margin, negative number otherwise.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => _.SetPositionMarginAsync(Settlement, contract, change, ct);

    /// <summary>
    /// Update position leverage
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="leverage">New position leverage</param>
    /// <param name="crossLeverageLimit">Cross margin leverage(valid only when leverage is 0)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        => _.SetLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);
    
    /// <summary>
    /// Update position risk limit
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="riskLimit">New position risk limit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => _.SetRiskLimitAsync(Settlement, contract, riskLimit, ct);

    /// <summary>
    /// Enable or disable dual mode
    /// Before setting dual mode, make sure all positions are closed and no orders are open
    /// </summary>
    /// <param name="dualMode">Whether to enable dual mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesBalance>> SetDualModeAsync(bool dualMode, CancellationToken ct = default)
        => _.SetDualModeAsync(Settlement, dualMode, ct);

    /// <summary>
    /// Retrieve position detail in dual mode
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPosition>>> GetDualModePositionsAsync(string contract, CancellationToken ct = default)
        => _.GetDualModePositionsAsync(Settlement, contract, ct);

    /// <summary>
    /// Retrieve position detail in dual mode
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="side">Long or short position</param>
    /// <param name="change">Margin change. Use positive number to increase margin, negative number otherwise.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetDualModeMarginAsync(string contract, GateFuturesDualModeSide side, decimal change, CancellationToken ct = default)
        => _.SetDualModeMarginAsync(Settlement, contract, side, change, ct);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="leverage"></param>
    /// <param name="crossLeverageLimit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetDualModeLeverageAsync(string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        => _.SetDualModeLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    /// <summary>
    /// Update position risk limit in dual mode
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="riskLimit">New position risk limit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetDualModeRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => _.SetDualModeRiskLimitAsync(Settlement, contract, riskLimit, ct);

    /// <summary>
    /// Create a futures order
    /// 
    /// Creating futures orders requires size, which is number of contracts instead of currency amount. You can use quanto_multiplier in contract detail response to know how much currency 1 size contract represents
    /// Zero-filled order cannot be retrieved 10 minutes after order cancellation. You will get a 404 not found for such orders
    /// Set reduce_only to true can keep the position from changing side when reducing position size
    /// In single position mode, to close a position, you need to set size to 0 and close to true
    /// In dual position mode, to close one side position, you need to set auto_size side, reduce_only to true and size to 0
    /// Set stp_act to decide the strategy of self-trade prevention. For detailed usage, refer to the stp_act parameter in request body
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="size">Order size. Specify positive number to make a bid, and negative number to ask</param>
    /// <param name="iceberg">Display size for iceberg order. 0 for non-iceberg. Note that you will have to pay the taker fee for the hidden size</param>
    /// <param name="price">Order price. 0 for market order with tif set as ioc</param>
    /// <param name="close">Set as true to close the position, with size set to 0</param>
    /// <param name="reduceOnly">Set as true to be reduce-only order</param>
    /// <param name="clientOrderId">User defined information. If not empty, must follow the rules below:</param>
    /// <param name="timeInForce">Time in force</param>
    /// <param name="autoSize">Set side to close dual-mode position. close_long closes the long side; while close_short the short one. Note size also needs to be set to 0</param>
    /// <param name="selfTradeAction">Self-Trading Prevention Action. Users can use this field to set self-trade prevetion strategies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(string contract, long size, long iceberg = 0, decimal? price = null, bool? close = null, bool? reduceOnly = null, string clientOrderId = null, GateFuturesTimeInForce? timeInForce = null, GateFuturesOrderAutoSize? autoSize = null, GateFuturesSelfTradeAction? selfTradeAction = null, CancellationToken ct = default)
        => _.PlaceOrderAsync(Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, selfTradeAction, ct);

    /// <summary>
    /// Create a futures order
    /// 
    /// Creating futures orders requires size, which is number of contracts instead of currency amount. You can use quanto_multiplier in contract detail response to know how much currency 1 size contract represents
    /// Zero-filled order cannot be retrieved 10 minutes after order cancellation. You will get a 404 not found for such orders
    /// Set reduce_only to true can keep the position from changing side when reducing position size
    /// In single position mode, to close a position, you need to set size to 0 and close to true
    /// In dual position mode, to close one side position, you need to set auto_size side, reduce_only to true and size to 0
    /// Set stp_act to decide the strategy of self-trade prevention. For detailed usage, refer to the stp_act parameter in request body
    /// </summary>
    /// <param name="request">Order Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(GateFuturesOrderRequest request, CancellationToken ct = default)
        => _.PlaceOrderAsync(Settlement, request, ct);

    /// <summary>
    /// List futures orders
    /// Zero-fill order cannot be retrieved for 10 minutes after cancellation
    /// Historical orders, by default, only data within the past 6 months is supported. If you need to query data for a longer period, please use GET /futures/{settle}/orders_timerange.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="status">Only list the orders with this status</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="lastId">Specify the starting point for this list based on a previously retrieved id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(string contract, GateFuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetOrdersAsync(Settlement, contract, status, limit, offset, lastId, ct);

    /// <summary>
    /// Cancel all open orders matched
    /// Zero-filled order cannot be retrieved 10 minutes after order cancellation
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(string contract, GateFuturesOrderSide? side = null, CancellationToken ct = default)
        => _.CancelOrdersAsync(Settlement, contract, side, ct);

    /// <summary>
    /// Create a batch of futures orders
    /// 
    /// Up to 10 orders per request
    /// If any of the order's parameters are missing or in the wrong format, all of them will not be executed, and a http status 400 error will be returned directly
    /// If the parameters are checked and passed, all are executed. Even if there is a business logic error in the middle (such as insufficient funds), it will not affect other execution orders
    /// The returned result is in array format, and the order corresponds to the orders in the request body
    /// In the returned result, the succeeded field of type bool indicates whether the execution was successful or not
    /// If the execution is successful, the normal order content is included; if the execution fails, the label field is included to indicate the cause of the error
    /// In the rate limiting, each order is counted individually
    /// </summary>
    /// <param name="requests">Order Requests</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesBatchOrder>>> PlaceOrdersAsync(IEnumerable<GateFuturesOrderRequest> requests, CancellationToken ct = default)
        => _.PlaceOrdersAsync(Settlement, requests, ct);

    /// <summary>
    /// Get a single order
    /// 
    /// Zero-fill order cannot be retrieved for 10 minutes after cancellation
    /// Historical orders, by default, only data within the past 6 months is supported.
    /// </summary>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => _.GetOrderAsync(Settlement, orderId, clientOrderId, ct);

    /// <summary>
    /// Cancel a single order
    /// </summary>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => _.CancelOrderAsync(Settlement, orderId, clientOrderId, ct);

    /// <summary>
    /// Amend an order
    /// </summary>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="size">New order size, including filled part.</param>
    /// <param name="price">New order price.</param>
    /// <param name="amendText">Custom info during amending order</param>
    /// <param name="businessInfo">Users can annotate this modification with information.</param>
    /// <param name="bbo">Users are able to modify the offer price manually.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrder>> AmendOrderAsync(long? orderId = null, string clientOrderId = null, long? size = null, decimal? price = null, string amendText = null, string businessInfo = null, string bbo = null, CancellationToken ct = default)
        => _.AmendOrderAsync(Settlement, orderId, clientOrderId, size, price, amendText, businessInfo, bbo, ct);

    /// <summary>
    /// List personal trading history
    /// By default, only data within the past 6 months is supported. If you need to query data for a longer period, please use GET /futures/{settle}/my_trades_timerange.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="orderId"></param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="lastId">Specify the starting point for this list based on a previously retrieved id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetUserTradesAsync(Settlement, contract, orderId, limit, offset, lastId, ct);

    /// <summary>
    /// List personal trading history by time range
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="role">Query role, maker or taker.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract, DateTime from, DateTime to, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetUserTradesAsync(Settlement, contract, from, to, role, limit, offset, ct);

    /// <summary>
    /// List personal trading history by time range
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="role">Query role, maker or taker.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? from = null, long? to = null, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetUserTradesAsync(Settlement, contract, from, to, role, limit, offset, ct);

    /// <summary>
    /// List position close history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="side">Query side. long or shot</param>
    /// <param name="pnl">Query profit or loss</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(string contract, DateTime from, DateTime to, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPositionClosesAsync(Settlement, contract, from, to, side, pnl, limit, offset, ct);

    /// <summary>
    /// List position close history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="side">Query side. long or shot</param>
    /// <param name="pnl">Query profit or loss</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(string contract = null, long? from = null, long? to = null, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPositionClosesAsync(Settlement, contract, from, to, side, pnl, limit, offset, ct);

    /// <summary>
    /// List liquidation history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="at">Specify a liquidation timestamp</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidationsAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => _.GetUserLiquidationsAsync(Settlement, contract, limit, at, ct);

    /// <summary>
    /// Countdown cancel orders
    /// When the timeout set by the user is reached, if there is no cancel or set a new countdown, the related pending orders will be automatically cancelled. This endpoint can be called repeatedly to set a new countdown or cancel the countdown. For example, call this endpoint at 30s intervals, each countdowntimeout is set to 30s. If this endpoint is not called again within 30 seconds, all pending orders on the specified market will be automatically cancelled, if no market is specified, all market pending orders will be cancelled. If the timeout is set to 0 within 30 seconds, the countdown timer will expire and the cacnel function will be cancelled.
    /// </summary>
    /// <param name="timeout">Countdown time, in seconds</param>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<DateTime>> CancelAllAsync(int timeout, string contract = null, CancellationToken ct = default)
        => _.CancelAllAsync(Settlement, timeout, contract, ct);

    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="triggerType">Take-profit/stop-loss types, which include:</param>
    /// <param name="triggerPriceType">Price type. 0 - latest deal price, 1 - mark price, 2 - index price</param>
    /// <param name="triggerStrategy">How the order will be triggered</param>
    /// <param name="triggerCondition">Trigger condition type</param>
    /// <param name="triggerPrice">Value of price on price triggered, or price gap on price gap triggered</param>
    /// <param name="triggerExpiration">How long (in seconds) to wait for the condition to be triggered before cancelling the order.</param>
    /// <param name="orderContract">Futures contract</param>
    /// <param name="orderPrice">Order price. Set to 0 to use market price</param>
    /// <param name="orderSize">Order size. Positive size means to buy, while negative one means to sell. Set to 0 to close the position</param>
    /// <param name="orderClose">Set to true if trying to close the position</param>
    /// <param name="orderTimeInForce">Time in force. If using market price, only ioc is supported.</param>
    /// <param name="orderClientOrderId">The source of the order, including:</param>
    /// <param name="orderReduceOnly">Set to true to create a reduce-only order</param>
    /// <param name="orderAutoSize">Set side to close dual-mode position. close_long closes the long side; while close_short the short one. Note size also needs to be set to 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
            // Type
            GateFuturesTriggerType triggerType,

            // Trigger
            GateFuturesTriggerPrice triggerPriceType,
            GateFuturesTriggerStrategy triggerStrategy,
            GateSpotTriggerCondition triggerCondition,
            decimal triggerPrice,
            TimeSpan triggerExpiration,

            // Initial Order
            string orderContract,
            decimal orderPrice,
            long orderSize,
            bool orderClose,
            GateFuturesTimeInForce orderTimeInForce,
            string orderClientOrderId,
            bool orderReduceOnly,
            GateFuturesOrderAutoSize orderAutoSize,

            // CancellationToken
            CancellationToken ct = default)
        => _.PlacePriceTriggeredOrderAsync(
            // Settlement
            Settlement,

            // Type
            triggerType,

            // Trigger
            triggerPriceType,
            triggerStrategy,
            triggerCondition,
            triggerPrice,
            triggerExpiration,

            // Initial Order
            orderContract,
            orderPrice,
            orderSize,
            orderClose,
            orderTimeInForce,
            orderClientOrderId,
            orderReduceOnly,
            orderAutoSize,

            // CancellationToken
            ct);

    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="request">Order Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
        => _.PlacePriceTriggeredOrderAsync(Settlement, request, ct);

    /// <summary>
    /// List all auto orders
    /// </summary>
    /// <param name="status">Only list the orders with this status</param>
    /// <param name="contract">Futures contract</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter status, string contract = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPriceTriggeredOrdersAsync(Settlement, status, contract, limit, offset, ct);

    /// <summary>
    /// Cancel all open orders
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(string contract = null, CancellationToken ct = default)
        => _.CancelPriceTriggeredOrdersAsync(Settlement, contract, ct);

    /// <summary>
    /// Get a price-triggered order
    /// </summary>
    /// <param name="orderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => _.GetPriceTriggeredOrderAsync(Settlement, orderId, ct);

    /// <summary>
    /// cancel a price-triggered order
    /// </summary>
    /// <param name="orderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => _.CancelPriceTriggeredOrderAsync(Settlement, orderId, ct);
}