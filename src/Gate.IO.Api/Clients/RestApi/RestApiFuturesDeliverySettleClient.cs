using Gate.IO.Api.Models.RestApi.Futures;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiFuturesDeliverySettleClient
{
    internal RestApiFuturesDeliveryClient MainClient { get; }
    internal FuturesDeliverySettle Settlement { get; }

    internal RestApiFuturesDeliverySettleClient(RestApiFuturesDeliveryClient main, FuturesDeliverySettle settle)
    {
        MainClient = main;
        Settlement = settle;
    }

    public async Task<RestCallResult<IEnumerable<DeliveryContract>>> GetContractsAsync(CancellationToken ct = default)
        => await MainClient.GetContractsAsync(this.Settlement, ct).ConfigureAwait(false);

    public async Task<RestCallResult<DeliveryContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => await MainClient.GetContractAsync(this.Settlement, contract, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => await MainClient.GetOrderBookAsync(this.Settlement, contract, interval, limit, withId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(string contract, DateTime from, DateTime to, int limit = 100,  long? lastId = null, CancellationToken ct = default)
        => await MainClient.GetTradesAsync(this.Settlement, contract, from, to, limit, lastId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(string contract, long? from = null, long? to = null, int limit = 100, long? lastId = null, CancellationToken ct = default)
        => await MainClient.GetTradesAsync(this.Settlement, contract, from, to, limit, lastId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => await MainClient.GetCandlesticksAsync(this.Settlement, contract, interval, from, to, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => await MainClient.GetCandlesticksAsync(this.Settlement, contract, interval, from, to, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<DeliveryTicker>>> GetTickersAsync(string contract = "", CancellationToken ct = default)
        => await MainClient.GetTickersAsync(this.Settlement, contract, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => await MainClient.GetInsuranceHistoryAsync(this.Settlement, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesAccount>>> GetAccountAsync(CancellationToken ct = default)
        => await MainClient.GetAccountAsync(this.Settlement, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesAccountBookType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => await MainClient.GetAccountBookAsync(this.Settlement, type, from, to, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesAccountBookType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => await MainClient.GetAccountBookAsync(this.Settlement, type, from, to, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesPosition>>> GetPositionsAsync(CancellationToken ct = default)
        => await MainClient.GetPositionsAsync(this.Settlement, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => await MainClient.GetPositionAsync(this.Settlement, contract, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => await MainClient.SetPositionMarginAsync(this.Settlement, contract, change, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesPosition>> SetLeverageAsync(string contract, int leverage, CancellationToken ct = default)
        => await MainClient.SetLeverageAsync(this.Settlement, contract, leverage,  ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => await MainClient.SetRiskLimitAsync(this.Settlement, contract, riskLimit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(string contract, long size, long iceberg = 0, decimal? price = null, bool? close = null, bool? reduceOnly = null, string clientOrderId = "", FuturesTimeInForce? timeInForce = null, FuturesOrderAutoSize? autoSize = null, CancellationToken ct = default)
        => await MainClient.PlaceOrderAsync(this.Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(FuturesOrderRequest request, CancellationToken ct = default)
        => await MainClient.PlaceOrderAsync(this.Settlement, request, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesOrder>>> GetOrdersAsync(string contract, FuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
        => await MainClient.GetOrdersAsync(this.Settlement, contract, status, limit, offset, lastId, countTotal, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesOrder>>> CancelOpenOrdersAsync(string contract, FuturesOrderSide side, CancellationToken ct = default)
        => await MainClient.CancelOpenOrdersAsync(this.Settlement, contract, side, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesOrder>> GetOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => await MainClient.GetOrderAsync(this.Settlement, orderId, clientOrderId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => await MainClient.CancelOrderAsync(this.Settlement, orderId, clientOrderId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(string contract = "", long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
        => await MainClient.GetUserTradesAsync(this.Settlement, contract, orderId, limit, offset, lastId, countTotal, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesClosedPosition>>> GetClosedPositionsAsync(string contract = "", int limit = 100, CancellationToken ct = default)
        => await MainClient.GetClosedPositionsAsync(this.Settlement, contract, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesUserLiquidate>>> GetUserLiquidatesAsync(string contract = "", int limit = 100, long? at = null, CancellationToken ct = default)
        => await MainClient.GetUserLiquidatesAsync(this.Settlement, contract, limit, at, ct).ConfigureAwait(false);
    
    public async Task<RestCallResult<IEnumerable<DeliverySettlement>>> GetUserSettlementsAsync(string contract = "", int limit = 100, long? at = null, CancellationToken ct = default)
        => await MainClient.GetUserSettlementsAsync(this.Settlement, contract, limit, at, ct).ConfigureAwait(false);

    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesTriggerOrderType type, FuturesTriggerOrderPriceType triggerPriceType, FuturesTriggerOrderStrategyType triggerStrategyType, decimal triggerPrice, PriceTriggerCondition triggerCondition, TimeSpan triggerExpiration, string orderContract, decimal orderPrice, long orderSize, CancellationToken ct = default)
        => await MainClient.PlacePriceTriggeredOrderAsync(this.Settlement, type, triggerPriceType, triggerStrategyType, triggerPrice, triggerCondition, triggerExpiration, orderContract, orderPrice, orderSize, ct).ConfigureAwait(false);

    public async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesTriggerOrderRequest request, CancellationToken ct = default)
        => await MainClient.PlacePriceTriggeredOrderAsync(this.Settlement, request, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(PriceTriggerFilter status, string contract = "", int limit = 100, int offset = 0, CancellationToken ct = default)
        => await MainClient.GetPriceTriggeredOrdersAsync(this.Settlement, status, contract, limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(string contract, CancellationToken ct = default)
        => await MainClient.CancelPriceTriggeredOrdersAsync(this.Settlement, contract, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => await MainClient.GetPriceTriggeredOrderAsync(this.Settlement, orderId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => await MainClient.CancelPriceTriggeredOrderAsync(this.Settlement, orderId, ct).ConfigureAwait(false);
}