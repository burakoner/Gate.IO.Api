namespace Gate.IO.Api.Futures;

public class GateFuturesPerpetualSettleRestApiClient
{
    internal GateFuturesSettlement Settlement { get; }
    internal GateFuturesPerpetualRestApiClient _ { get; }

    internal GateFuturesPerpetualSettleRestApiClient(GateFuturesPerpetualRestApiClient main, GateFuturesSettlement settle)
    {
        _ = main;
        Settlement = settle;
    }

    public Task<RestCallResult<List<GateFuturesContract>>> GetContractsAsync(int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetContractsAsync(Settlement, limit, offset, ct);

    public Task<RestCallResult<GateFuturesContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => _.GetContractAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => _.GetOrderBookAsync(Settlement, contract, interval, limit, withId, ct);

    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(string contract = null, CancellationToken ct = default)
        => _.GetTickersAsync(Settlement, contract, ct);

    public Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetFundingRateHistoryAsync(settle, contract, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetFundingRateHistoryAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => _.GetInsuranceHistoryAsync(Settlement, limit, ct);

    public Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(string contract, GateFuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
        => _.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    public Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(string contract, GateFuturesStatsInterval? interval = null, long? from = null, int limit = 100, CancellationToken ct = default)
        => _.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    public Task<RestCallResult<GateFuturesIndexConstituents>> GetIndexConstituentsAsync(string index, CancellationToken ct = default)
        => _.GetIndexConstituentsAsync(Settlement, index, ct);

    public Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidatesAsync(string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => _.GetLiquidatesAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidatesAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => _.GetLiquidatesAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<GateFuturesBalance>> GetBalanceAsync(CancellationToken ct = default)
        => _.GetBalanceAsync(Settlement, ct);

    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesBalanceType type, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetBalanceHistoryAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesBalanceType? type = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetBalanceHistoryAsync(settle, contract, from, to, type, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(bool? holding = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPositionsAsync(Settlement, holding, limit, offset, ct);

    public Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => _.GetPositionAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => _.SetPositionMarginAsync(Settlement, contract, change, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        => _.SetLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => _.SetRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<GateFuturesBalance>> SetDualModeAsync(bool dualMode, CancellationToken ct = default)
        => _.SetDualModeAsync(Settlement, dualMode, ct);

    public Task<RestCallResult<List<GateFuturesPosition>>> GetDualModePositionsAsync(string contract, CancellationToken ct = default)
        => _.GetDualModePositionsAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetDualModeMarginAsync(string contract, GateFuturesDualModeSide side, decimal change, CancellationToken ct = default)
        => _.SetDualModeMarginAsync(Settlement, contract, side, change, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetDualModeLeverageAsync(string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        => _.SetDualModeLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetDualModeRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => _.SetDualModeRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(string contract, long size, long iceberg = 0, decimal? price = null, bool? close = null, bool? reduceOnly = null, string clientOrderId = null, GateFuturesTimeInForce? timeInForce = null, GateFuturesOrderAutoSize? autoSize = null, GateFuturesSelfTradeAction? selfTradeAction = null, CancellationToken ct = default)
        => _.PlaceOrderAsync(Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, selfTradeAction, ct);

    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(GateFuturesOrderRequest request, CancellationToken ct = default)
        => _.PlaceOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(string contract, GateFuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetOrdersAsync(Settlement, contract, status, limit, offset, lastId, ct);

    public Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(string contract, GateFuturesOrderSide? side = null, CancellationToken ct = default)
        => _.CancelOrdersAsync(Settlement, contract, side, ct);

    public Task<RestCallResult<List<GateFuturesBatchOrder>>> PlaceOrdersAsync(IEnumerable<GateFuturesOrderRequest> requests, CancellationToken ct = default)
        => _.PlaceOrdersAsync(Settlement, requests, ct);

    public Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => _.GetOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => _.CancelOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<GateFuturesOrder>> AmendOrderAsync(long? orderId = null, string clientOrderId = null, long? size = null, decimal? price = null, string amendText = null, string businessInfo = null, string bbo = null, CancellationToken ct = default)
        => _.AmendOrderAsync(Settlement, orderId, clientOrderId, size, price, amendText, businessInfo, bbo, ct);

    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => _.GetUserTradesAsync(Settlement, contract, orderId, limit, offset, lastId, ct);

    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract, DateTime from, DateTime to, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetUserTradesAsync(Settlement, contract, from, to, role, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? from = null, long? to = null, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetUserTradesAsync(Settlement, contract, from, to, role, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPositionClosesAsync(Settlement, contract, from, to, side, pnl, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPositionClosesAsync(Settlement, contract, from, to, side, pnl, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidatesAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => _.GetUserLiquidatesAsync(Settlement, contract, limit, at, ct);

    public Task<RestCallResult<DateTime>> CancelAllAsync(int timeout, string contract = null, CancellationToken ct = default)
        => _.CancelAllAsync(Settlement, timeout, contract, ct);

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

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
        => _.PlacePriceTriggeredOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter status, string contract = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => _.GetPriceTriggeredOrdersAsync(Settlement, status, contract, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(string contract=null, CancellationToken ct = default)
        => _.CancelPriceTriggeredOrdersAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => _.GetPriceTriggeredOrderAsync(Settlement, orderId, ct);

    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => _.CancelPriceTriggeredOrderAsync(Settlement, orderId, ct);
}