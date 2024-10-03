namespace Gate.IO.Api.Futures;

public class GateFuturesPerpetualSettleRestApiClient
{
    internal GateFuturesSettlement Settlement { get; }
    internal GateFuturesPerpetualRestApiClient MainClient { get; }

    internal GateFuturesPerpetualSettleRestApiClient(GateFuturesPerpetualRestApiClient main, GateFuturesSettlement settle)
    {
        MainClient = main;
        Settlement = settle;
    }

    public Task<RestCallResult<List<GateFuturesContract>>> GetContractsAsync(int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetContractsAsync(Settlement, limit, offset, ct);

    public Task<RestCallResult<GateFuturesContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => MainClient.GetContractAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => MainClient.GetOrderBookAsync(Settlement, contract, interval, limit, withId, ct);

    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(string contract = null, CancellationToken ct = default)
        => MainClient.GetTickersAsync(Settlement, contract, ct);

    public Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetFundingRateHistoryAsync(settle, contract, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetFundingRateHistoryAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => MainClient.GetInsuranceHistoryAsync(Settlement, limit, ct);

    public Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(string contract, FuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
        => MainClient.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    public Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(string contract, FuturesStatsInterval? interval = null, long? from = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    public Task<RestCallResult<GateFuturesIndexConstituents>> GetIndexConstituentsAsync(string index, CancellationToken ct = default)
        => MainClient.GetIndexConstituentsAsync(Settlement, index, ct);

    public Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidatesAsync(string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetLiquidatesAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidatesAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetLiquidatesAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<GateFuturesBalance>> GetBalanceAsync(CancellationToken ct = default)
        => MainClient.GetBalanceAsync(Settlement, ct);

    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesBalanceType type, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetBalanceHistoryAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesBalanceType? type = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetBalanceHistoryAsync(settle, contract, from, to, type, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(bool? holding = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPositionsAsync(Settlement, holding, limit, offset, ct);

    public Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => MainClient.GetPositionAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => MainClient.SetPositionMarginAsync(Settlement, contract, change, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(string contract, int leverage, int? crossLeverageLimit = null, CancellationToken ct = default)
        => MainClient.SetLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => MainClient.SetRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<GateFuturesBalance>> SetDualModeAsync(bool dualMode, CancellationToken ct = default)
        => MainClient.SetDualModeAsync(Settlement, dualMode, ct);

    public Task<RestCallResult<GateFuturesPosition>> GetDualModePositionAsync(string contract, CancellationToken ct = default)
        => MainClient.GetDualModePositionAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetDualModePositionMarginAsync(string contract, FuturesDualModeSide side, decimal change, CancellationToken ct = default)
        => MainClient.SetDualModePositionMarginAsync(Settlement, contract, side, change, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetDualModeLeverageAsync(string contract, int leverage, int? crossLeverageLimit = null, CancellationToken ct = default)
        => MainClient.SetDualModeLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetDualModeRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => MainClient.SetDualModeRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(string contract, long size, long iceberg = 0, decimal? price = null, bool? close = null, bool? reduceOnly = null, string clientOrderId = null, FuturesTimeInForce? timeInForce = null, FuturesOrderAutoSize? autoSize = null, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, ct);

    public Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(FuturesOrderRequest request, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<List<FuturesOrder>>> GetOrdersAsync(string contract, FuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetOrdersAsync(Settlement, contract, status, limit, offset, lastId, ct);

    public Task<RestCallResult<List<FuturesOrder>>> CancelOpenOrdersAsync(string contract, FuturesOrderSide side, CancellationToken ct = default)
        => MainClient.CancelOpenOrdersAsync(Settlement, contract, side, ct);

    public Task<RestCallResult<List<FuturesBatchOrder>>> PlaceOrderAsync(IEnumerable<FuturesOrderRequest> requests, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, requests, ct);

    public Task<RestCallResult<FuturesOrder>> GetOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.GetOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<FuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.CancelOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<FuturesOrder>> AmendOrderAsync(long? orderId = null, string clientOrderId = null, decimal? price = null, long? size = null, CancellationToken ct = default)
        => MainClient.AmendOrderAsync(Settlement, orderId, clientOrderId, price, size, ct);

    public Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, orderId, limit, offset, lastId, ct);

    public Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<List<FuturesPositionClose>>> GetPositionClosesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPositionClosesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<List<FuturesPositionClose>>> GetPositionClosesAsync(string contract = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPositionClosesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<List<FuturesUserLiquidate>>> GetUserLiquidatesAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => MainClient.GetUserLiquidatesAsync(Settlement, contract, limit, at, ct);

    public Task<RestCallResult<DateTime>> CountdownCancelOrdersAsync(int timeout, string contract = null, CancellationToken ct = default)
        => MainClient.CountdownCancelOrdersAsync(Settlement, timeout, contract, ct);

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesTriggerOrderType type, FuturesTriggerOrderPriceType triggerPriceType, FuturesTriggerOrderStrategyType triggerStrategyType, decimal triggerPrice, GateSpotTriggerCondition triggerCondition, TimeSpan triggerExpiration, string orderContract, decimal orderPrice, long orderSize, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, type, triggerPriceType, triggerStrategyType, triggerPrice, triggerCondition, triggerExpiration, orderContract, orderPrice, orderSize, ct);

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesTriggerOrderRequest request, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<List<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter status, string contract = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrdersAsync(Settlement, status, contract, limit, offset, ct);

    public Task<RestCallResult<List<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(string contract, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrdersAsync(Settlement, contract, ct);

    public Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrderAsync(Settlement, orderId, ct);

    public Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrderAsync(Settlement, orderId, ct);
}