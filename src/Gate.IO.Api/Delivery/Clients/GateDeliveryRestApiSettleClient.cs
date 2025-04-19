namespace Gate.IO.Api.Delivery;

/// <summary>
/// Gate.IO Futures Delivery Settlement REST API client
/// </summary>
public class GateDeliveryRestApiSettleClient
{
    internal GateDeliverySettlement Settlement { get; }
    internal GateDeliveryRestApiClient MainClient { get; }

    internal GateDeliveryRestApiSettleClient(GateDeliveryRestApiClient main, GateDeliverySettlement settle)
    {
        MainClient = main;
        Settlement = settle;
    }
    
    /// <summary>
    /// List all futures contracts
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateDeliveryContract>>> GetContractsAsync(CancellationToken ct = default)
        => MainClient.GetContractsAsync(Settlement, ct);
    
    /// <summary>
    /// Get a single contract
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateDeliveryContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => MainClient.GetContractAsync(Settlement, contract, ct);
    
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
        => MainClient.GetOrderBookAsync(Settlement, contract, interval, limit, withId, ct);
    
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
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);
    
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
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);
    
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
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);
    
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
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);
    
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
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);
    
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
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);
    
    /// <summary>
    /// List futures tickers
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(string contract = null, CancellationToken ct = default)
        => MainClient.GetTickersAsync(Settlement, contract, ct);
    
    /// <summary>
    /// Futures insurance balance history
    /// </summary>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => MainClient.GetInsuranceHistoryAsync(Settlement, limit, ct);
    
    /// <summary>
    /// Query futures account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesBalance>>> GetBalancesAsync(CancellationToken ct = default)
        => MainClient.GetBalancesAsync(Settlement, ct);
    
    /// <summary>
    /// Query account book
    /// If the contract field is provided, it can only filter records that include this field after 2023-10-30.
    /// </summary>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="type">Changing Type</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesBalanceChangeType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetBalanceHistoryAsync(Settlement, type, from, to, limit, ct);
    
    /// <summary>
    /// Query account book
    /// If the contract field is provided, it can only filter records that include this field after 2023-10-30.
    /// </summary>
    /// <param name="from">Specify starting time in Unix seconds. If not specified, to and limit will be used to limit response items.</param>
    /// <param name="to">Specify end time in Unix seconds, default to current time</param>
    /// <param name="type">Changing Type</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesBalanceChangeType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetBalanceHistoryAsync(Settlement, type, from, to, limit, ct);
    
    /// <summary>
    /// List all positions of a user
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(CancellationToken ct = default)
        => MainClient.GetPositionsAsync(Settlement, ct);
    
    /// <summary>
    /// Get single position
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => MainClient.GetPositionAsync(Settlement, contract, ct);
    
    /// <summary>
    /// Update position margin
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="change">Margin change. Use positive number to increase margin, negative number otherwise.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => MainClient.SetPositionMarginAsync(Settlement, contract, change, ct);
    
    /// <summary>
    /// Update position leverage
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="leverage">New position leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(string contract, decimal leverage, CancellationToken ct = default)
        => MainClient.SetLeverageAsync(Settlement, contract, leverage, ct);
    
    /// <summary>
    /// Update position risk limit
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="riskLimit">New position risk limit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => MainClient.SetRiskLimitAsync(Settlement, contract, riskLimit, ct);
    
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
    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(
        string contract,
        long size,
        long? iceberg = null,
        decimal? price = null,
        bool? close = null,
        bool? reduceOnly = null,
        string clientOrderId = null,
        GateFuturesTimeInForce? timeInForce = null,
        GateFuturesOrderAutoSize? autoSize = null,
        GateFuturesSelfTradeAction? selfTradeAction = null,
        CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, selfTradeAction, ct);
    
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
        => MainClient.PlaceOrderAsync(Settlement, request, ct);
    
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
    /// <param name="countTotal">Whether to return total number matched. Default to 0(no return)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(string contract, GateFuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
        => MainClient.GetOrdersAsync(Settlement, contract, status, limit, offset, lastId, countTotal, ct);
    
    /// <summary>
    /// Cancel all open orders matched
    /// Zero-filled order cannot be retrieved 10 minutes after order cancellation
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="side">All bids or asks. Both included if not specified</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(string contract, GateFuturesOrderSide side, CancellationToken ct = default)
        => MainClient.CancelOrdersAsync(Settlement, contract, side, ct);
    
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
        => MainClient.GetOrderAsync(Settlement, orderId, clientOrderId, ct);
    
    /// <summary>
    /// Cancel a single order
    /// </summary>
    /// <param name="orderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="clientOrderId">Order ID returned, or user custom ID(i.e., text field).</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.CancelOrderAsync(Settlement, orderId, clientOrderId, ct);
    
    /// <summary>
    /// List personal trading history
    /// By default, only data within the past 6 months is supported. If you need to query data for a longer period, please use GET /futures/{settle}/my_trades_timerange.
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="orderId"></param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="lastId">Specify the starting point for this list based on a previously retrieved id</param>
    /// <param name="countTotal">Whether to return total number matched. Default to 0(no return)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, orderId, limit, offset, lastId, countTotal, ct);
    
    /// <summary>
    /// List position close history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(string contract = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetPositionClosesAsync(Settlement, contract, limit, ct);
    
    /// <summary>
    /// List liquidation history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="at">Specify a liquidation timestamp</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidationsAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => MainClient.GetUserLiquidationsAsync(Settlement, contract, limit, at, ct);

    /// <summary>
    /// List settlement history
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="at">Specify a settlement timestamp</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateDeliveryUserSettlement>>> GetUserSettlementsAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => MainClient.GetUserSettlementsAsync(Settlement, contract, limit, at, ct);
    
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
        => MainClient.PlacePriceTriggeredOrderAsync(
            Settlement, triggerType, triggerPriceType, triggerStrategy, triggerCondition, triggerPrice, triggerExpiration,
            orderContract, orderPrice, orderSize, orderClose, orderTimeInForce, orderClientOrderId, orderReduceOnly, orderAutoSize, ct);
    
    /// <summary>
    /// Create a price-triggered order
    /// </summary>
    /// <param name="request">Order Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, request, ct);
    
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
        => MainClient.GetPriceTriggeredOrdersAsync(Settlement, status, contract, limit, offset, ct);
    
    /// <summary>
    /// Cancel all open orders
    /// </summary>
    /// <param name="contract">Futures contract</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(string contract, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrdersAsync(Settlement, contract, ct);
    
    /// <summary>
    /// Get a price-triggered order
    /// </summary>
    /// <param name="orderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrderAsync(Settlement, orderId, ct);
    
    /// <summary>
    /// cancel a price-triggered order
    /// </summary>
    /// <param name="orderId">Retrieve the data of the order with the specified ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrderAsync(Settlement, orderId, ct);
}