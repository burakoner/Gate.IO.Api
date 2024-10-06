using Gate.IO.Api.Models.StreamApi.Futures;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiFuturesPerpetualSettleClient
{
    internal GateFuturesSettlement Settlement { get; }
    internal StreamApiFuturesPerpetualClient MainClient { get; }

    internal StreamApiFuturesPerpetualSettleClient(StreamApiFuturesPerpetualClient main, GateFuturesSettlement settle)
    {
        MainClient = main;
        Settlement = settle;
    }

    public async Task UnsubscribeAsync(int subscriptionId)
    => await MainClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task UnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await MainClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task UnsubscribeAllAsync()
        => await MainClient.UnsubscribeAllAsync().ConfigureAwait(false);

    public async Task<CallResult<GateStreamLatency>> PingAsync()
        => await MainClient.PingAsync(this.Settlement).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<FuturesStreamPerpetualTicker>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToTickersAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);

    /*
    public async Task<CallResult<GateStreamResponse<GateStreamStatus>>> UnsubscribeFromTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateStreamResponse<GateStreamStatus>>> onMessage, CancellationToken ct = default)
        => await MainClient.UnsubscribeFromTickersAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);
    */

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<FuturesStreamTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToTradesAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<FuturesStreamBookTicker>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToOrderBookTickersAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string contract, int frequency, int level, Action<WebSocketDataEvent<FuturesStreamBookDifference>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToOrderBookDifferencesAsync(this.Settlement, contract, frequency, level, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(string contract, /*int interval,*/ int limit, Action<WebSocketDataEvent<FuturesStreamBookSnapshot>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToOrderBookSnapshotsAsync(this.Settlement, contract, /*int interval,*/  limit, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, Action<WebSocketDataEvent<FuturesStreamCandlestick>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToCandlesticksAsync(this.Settlement, "mark_", contract, interval, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToIndexPriceCandlesticksAsync(string contract, GateFuturesCandlestickInterval interval, Action<WebSocketDataEvent<FuturesStreamCandlestick>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToCandlesticksAsync(this.Settlement, "index_", contract, interval, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(long userId, Action<WebSocketDataEvent<GateFuturesOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserOrdersAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(long userId, string contract, Action<WebSocketDataEvent<GateFuturesOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserOrdersAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(long userId, Action<WebSocketDataEvent<GateFuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserTradesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(long userId, string contract, Action<WebSocketDataEvent<GateFuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserTradesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidationsAsync(long userId, Action<WebSocketDataEvent<GateFuturesUserLiquidation>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserLiquidationsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidationsAsync(long userId, string contract, Action<WebSocketDataEvent<GateFuturesUserLiquidation>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserLiquidationsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserDeleveragesAsync(long userId, Action<WebSocketDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserDeleveragesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserDeleveragesAsync(long userId, string contract, Action<WebSocketDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserDeleveragesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(long userId, Action<WebSocketDataEvent<GateFuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionClosesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(long userId, string contract, Action<WebSocketDataEvent<GateFuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionClosesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserBalancesAsync(long userId, Action<WebSocketDataEvent<FuturesStreamBalance>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserBalancesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(long userId, Action<WebSocketDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserReduceRiskLimitsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(long userId, string contract, Action<WebSocketDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserReduceRiskLimitsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(long userId, Action<WebSocketDataEvent<GateFuturesPosition>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(long userId, string contract, Action<WebSocketDataEvent<GateFuturesPosition>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAutoOrdersAsync(long userId, Action<WebSocketDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserAutoOrdersAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAutoOrdersAsync(long userId, string contract, Action<WebSocketDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserAutoOrdersAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

}