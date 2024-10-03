namespace Gate.IO.Api.Futures;

public class GateFuturesPerpetualSettleRestApiClient
{
    internal FuturesPerpetualSettle Settlement { get; }
    internal GateFuturesPerpetualRestApiClient MainClient { get; }

    internal GateFuturesPerpetualSettleRestApiClient(GateFuturesPerpetualRestApiClient main, FuturesPerpetualSettle settle)
    {
        MainClient = main;
        Settlement = settle;
    }

    public Task<RestCallResult<IEnumerable<PerpetualContract>>> GetContractsAsync(CancellationToken ct = default)
        => MainClient.GetContractsAsync(Settlement, ct);

    public Task<RestCallResult<PerpetualContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => MainClient.GetContractAsync(Settlement, contract, ct);

    public Task<RestCallResult<FuturesOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => MainClient.GetOrderBookAsync(Settlement, contract, interval, limit, withId, ct);

    public Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    public Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, offset, lastId, ct);

    public Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetPremiumIndexCandlesticksAsync(string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetPremiumIndexCandlesticksAsync(string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetPremiumIndexCandlesticksAsync(Settlement, contract, interval, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<PerpetualTicker>>> GetTickersAsync(string contract = "", CancellationToken ct = default)
        => MainClient.GetTickersAsync(Settlement, contract, ct);

    public Task<RestCallResult<IEnumerable<FuturesFundingRate>>> GetFundingRateHistoryAsync(string contract, int limit = 100, CancellationToken ct = default)
        => MainClient.GetFundingRateHistoryAsync(Settlement, contract, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => MainClient.GetInsuranceHistoryAsync(Settlement, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesStats>>> GetStatsAsync(string contract, FuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
        => MainClient.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesStats>>> GetStatsAsync(string contract, FuturesStatsInterval interval, long? from = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetStatsAsync(Settlement, contract, interval, from, limit, ct);

    public Task<RestCallResult<FuturesIndexConstituents>> GetIndexConstituentsAsync(string index, CancellationToken ct = default)
        => MainClient.GetIndexConstituentsAsync(Settlement, index, ct);

    public Task<RestCallResult<IEnumerable<FuturesLiquidate>>> GetLiquidatesAsync(string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetLiquidatesAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesLiquidate>>> GetLiquidatesAsync(string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetLiquidatesAsync(Settlement, contract, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesAccount>>> GetAccountAsync(CancellationToken ct = default)
        => MainClient.GetAccountAsync(Settlement, ct);

    public Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesAccountBookType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetAccountBookAsync(Settlement, type, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesAccountBookType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetAccountBookAsync(Settlement, type, from, to, limit, ct);

    public Task<RestCallResult<IEnumerable<FuturesPosition>>> GetPositionsAsync(CancellationToken ct = default)
        => MainClient.GetPositionsAsync(Settlement, ct);

    public Task<RestCallResult<FuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => MainClient.GetPositionAsync(Settlement, contract, ct);

    public Task<RestCallResult<FuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => MainClient.SetPositionMarginAsync(Settlement, contract, change, ct);

    public Task<RestCallResult<FuturesPosition>> SetLeverageAsync(string contract, int leverage, int? crossLeverageLimit = null, CancellationToken ct = default)
        => MainClient.SetLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    public Task<RestCallResult<FuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => MainClient.SetRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<FuturesAccount>> SetDualModeAsync(bool dualMode, CancellationToken ct = default)
        => MainClient.SetDualModeAsync(Settlement, dualMode, ct);

    public Task<RestCallResult<FuturesPosition>> GetDualModePositionAsync(string contract, CancellationToken ct = default)
        => MainClient.GetDualModePositionAsync(Settlement, contract, ct);

    public Task<RestCallResult<FuturesPosition>> SetDualModePositionMarginAsync(string contract, FuturesDualModeSide side, decimal change, CancellationToken ct = default)
        => MainClient.SetDualModePositionMarginAsync(Settlement, contract, side, change, ct);

    public Task<RestCallResult<FuturesPosition>> SetDualModeLeverageAsync(string contract, int leverage, int? crossLeverageLimit = null, CancellationToken ct = default)
        => MainClient.SetDualModeLeverageAsync(Settlement, contract, leverage, crossLeverageLimit, ct);

    public Task<RestCallResult<FuturesPosition>> SetDualModeRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => MainClient.SetDualModeRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(string contract, long size, long iceberg = 0, decimal? price = null, bool? close = null, bool? reduceOnly = null, string clientOrderId = "", FuturesTimeInForce? timeInForce = null, FuturesOrderAutoSize? autoSize = null, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, ct);

    public Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(FuturesOrderRequest request, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<IEnumerable<FuturesOrder>>> GetOrdersAsync(string contract, FuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetOrdersAsync(Settlement, contract, status, limit, offset, lastId, ct);

    public Task<RestCallResult<IEnumerable<FuturesOrder>>> CancelOpenOrdersAsync(string contract, FuturesOrderSide side, CancellationToken ct = default)
        => MainClient.CancelOpenOrdersAsync(Settlement, contract, side, ct);

    public Task<RestCallResult<IEnumerable<FuturesBatchOrder>>> PlaceOrderAsync(IEnumerable<FuturesOrderRequest> requests, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, requests, ct);

    public Task<RestCallResult<FuturesOrder>> GetOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.GetOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<FuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.CancelOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<FuturesOrder>> AmendOrderAsync(long? orderId = null, string clientOrderId = null, decimal? price = null, long? size = null, CancellationToken ct = default)
        => MainClient.AmendOrderAsync(Settlement, orderId, clientOrderId, price, size, ct);

    public Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(string contract = "", long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, orderId, limit, offset, lastId, ct);

    public Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(string contract = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<IEnumerable<FuturesPositionClose>>> GetPositionClosesAsync(string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPositionClosesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<IEnumerable<FuturesPositionClose>>> GetPositionClosesAsync(string contract = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPositionClosesAsync(Settlement, contract, from, to, limit, offset, ct);

    public Task<RestCallResult<IEnumerable<FuturesUserLiquidate>>> GetUserLiquidatesAsync(string contract = "", int limit = 100, long? at = null, CancellationToken ct = default)
        => MainClient.GetUserLiquidatesAsync(Settlement, contract, limit, at, ct);

    public Task<RestCallResult<DateTime>> CountdownCancelOrdersAsync(int timeout, string contract = "", CancellationToken ct = default)
        => MainClient.CountdownCancelOrdersAsync(Settlement, timeout, contract, ct);

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesTriggerOrderType type, FuturesTriggerOrderPriceType triggerPriceType, FuturesTriggerOrderStrategyType triggerStrategyType, decimal triggerPrice, GateSpotTriggerCondition triggerCondition, TimeSpan triggerExpiration, string orderContract, decimal orderPrice, long orderSize, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, type, triggerPriceType, triggerStrategyType, triggerPrice, triggerCondition, triggerExpiration, orderContract, orderPrice, orderSize, ct);

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesTriggerOrderRequest request, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter status, string contract = "", int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrdersAsync(Settlement, status, contract, limit, offset, ct);

    public Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(string contract, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrdersAsync(Settlement, contract, ct);

    public Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrderAsync(Settlement, orderId, ct);

    public Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrderAsync(Settlement, orderId, ct);
}