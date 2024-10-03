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

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(int userId, Action<WebSocketDataEvent<FuturesOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserOrdersAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserOrdersAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(int userId, Action<WebSocketDataEvent<FuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserTradesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserTradesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidatesAsync(int userId, Action<WebSocketDataEvent<FuturesUserLiquidate>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserLiquidatesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidatesAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesUserLiquidate>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserLiquidatesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserDeleveragesAsync(int userId, Action<WebSocketDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserDeleveragesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserDeleveragesAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserDeleveragesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(int userId, Action<WebSocketDataEvent<FuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionClosesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionClosesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserBalancesAsync(int userId, Action<WebSocketDataEvent<FuturesStreamBalance>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserBalancesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(int userId, Action<WebSocketDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserReduceRiskLimitsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserReduceRiskLimitsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(int userId, Action<WebSocketDataEvent<GateFuturesPosition>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(int userId, string contract, Action<WebSocketDataEvent<GateFuturesPosition>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAutoOrdersAsync(int userId, Action<WebSocketDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserAutoOrdersAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAutoOrdersAsync(int userId, string contract, Action<WebSocketDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserAutoOrdersAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

}