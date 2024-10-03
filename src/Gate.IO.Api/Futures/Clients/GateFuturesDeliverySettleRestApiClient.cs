﻿namespace Gate.IO.Api.Futures;

public class GateFuturesDeliverySettleRestApiClient
{
    internal GateDeliverySettlement Settlement { get; }
    internal GateFuturesDeliveryRestApiClient MainClient { get; }

    internal GateFuturesDeliverySettleRestApiClient(GateFuturesDeliveryRestApiClient main, GateDeliverySettlement settle)
    {
        MainClient = main;
        Settlement = settle;
    }

    public Task<RestCallResult<List<DeliveryContract>>> GetContractsAsync(CancellationToken ct = default)
        => MainClient.GetContractsAsync(Settlement, ct);

    public Task<RestCallResult<DeliveryContract>> GetContractAsync(string contract, CancellationToken ct = default)
        => MainClient.GetContractAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
        => MainClient.GetOrderBookAsync(Settlement, contract, interval, limit, withId, ct);

    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, DateTime from, DateTime to, int limit = 100, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, lastId, ct);

    public Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(string contract, long? from = null, long? to = null, int limit = 100, long? lastId = null, CancellationToken ct = default)
        => MainClient.GetTradesAsync(Settlement, contract, from, to, limit, lastId, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "mark_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesCandlestick>>> GetIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetCandlesticksAsync(Settlement, "index_", contract, interval, from, to, limit, ct);

    public Task<RestCallResult<List<DeliveryTicker>>> GetTickersAsync(string contract = null, CancellationToken ct = default)
        => MainClient.GetTickersAsync(Settlement, contract, ct);

    public Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(int limit = 100, CancellationToken ct = default)
        => MainClient.GetInsuranceHistoryAsync(Settlement, limit, ct);

    public Task<RestCallResult<List<GateFuturesBalance>>> GetAccountAsync(CancellationToken ct = default)
        => MainClient.GetAccountAsync(Settlement, ct);

    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetAccountBookAsync(GateFuturesBalanceType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => MainClient.GetAccountBookAsync(Settlement, type, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesBalanceChange>>> GetAccountBookAsync(GateFuturesBalanceType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetAccountBookAsync(Settlement, type, from, to, limit, ct);

    public Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(CancellationToken ct = default)
        => MainClient.GetPositionsAsync(Settlement, ct);

    public Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(string contract, CancellationToken ct = default)
        => MainClient.GetPositionAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(string contract, decimal change, CancellationToken ct = default)
        => MainClient.SetPositionMarginAsync(Settlement, contract, change, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(string contract, decimal leverage, CancellationToken ct = default)
        => MainClient.SetLeverageAsync(Settlement, contract, leverage, ct);

    public Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(string contract, decimal riskLimit, CancellationToken ct = default)
        => MainClient.SetRiskLimitAsync(Settlement, contract, riskLimit, ct);

    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(string contract, long size, long iceberg = 0, decimal? price = null, bool? close = null, bool? reduceOnly = null, string clientOrderId = null, GateFuturesTimeInForce? timeInForce = null, GateFuturesOrderAutoSize? autoSize = null, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, contract, size, iceberg, price, close, reduceOnly, clientOrderId, timeInForce, autoSize, ct);

    public Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(GateFuturesOrderRequest request, CancellationToken ct = default)
        => MainClient.PlaceOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(string contract, GateFuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
        => MainClient.GetOrdersAsync(Settlement, contract, status, limit, offset, lastId, countTotal, ct);

    public Task<RestCallResult<List<GateFuturesOrder>>> CancelOpenOrdersAsync(string contract, GateFuturesOrderSide side, CancellationToken ct = default)
        => MainClient.CancelOpenOrdersAsync(Settlement, contract, side, ct);

    public Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.GetOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => MainClient.CancelOrderAsync(Settlement, orderId, clientOrderId, ct);

    public Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
        => MainClient.GetUserTradesAsync(Settlement, contract, orderId, limit, offset, lastId, countTotal, ct);

    public Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(string contract = null, int limit = 100, CancellationToken ct = default)
        => MainClient.GetPositionClosesAsync(Settlement, contract, limit, ct);

    public Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidatesAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => MainClient.GetUserLiquidatesAsync(Settlement, contract, limit, at, ct);

    public Task<RestCallResult<List<DeliverySettlement>>> GetUserSettlementsAsync(string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
        => MainClient.GetUserSettlementsAsync(Settlement, contract, limit, at, ct);

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesTriggerType type, GateFuturesTriggerPrice triggerPriceType, GateFuturesTriggerStrategy triggerStrategyType, decimal triggerPrice, GateSpotTriggerCondition triggerCondition, TimeSpan triggerExpiration, string orderContract, decimal orderPrice, long orderSize, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, type, triggerPriceType, triggerStrategyType, triggerPrice, triggerCondition, triggerExpiration, orderContract, orderPrice, orderSize, ct);

    public Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
        => MainClient.PlacePriceTriggeredOrderAsync(Settlement, request, ct);

    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter status, string contract = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrdersAsync(Settlement, status, contract, limit, offset, ct);

    public Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(string contract, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrdersAsync(Settlement, contract, ct);

    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.GetPriceTriggeredOrderAsync(Settlement, orderId, ct);

    public Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(long orderId, CancellationToken ct = default)
        => MainClient.CancelPriceTriggeredOrderAsync(Settlement, orderId, ct);
}