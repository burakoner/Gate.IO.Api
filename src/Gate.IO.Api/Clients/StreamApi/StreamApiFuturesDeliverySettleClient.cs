using Gate.IO.Api.Models.RestApi.Futures;
using Gate.IO.Api.Models.StreamApi;
using Gate.IO.Api.Models.StreamApi.Futures;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiFuturesDeliverySettleClient
{
    internal FuturesDeliverySettle Settlement { get; }
    internal StreamApiFuturesDeliveryClient MainClient { get; }

    internal StreamApiFuturesDeliverySettleClient(StreamApiFuturesDeliveryClient main, FuturesDeliverySettle settle)
    {
        MainClient = main;
        Settlement = settle;
    }

    public async Task UnsubscribeAsync(int subscriptionId)
    => await MainClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task UnsubscribeAsync(UpdateSubscription subscription)
        => await MainClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task UnsubscribeAllAsync()
        => await MainClient.UnsubscribeAllAsync().ConfigureAwait(false);

    public async Task<CallResult<GateStreamLatency>> PingAsync()
        => await MainClient.PingAsync(this.Settlement).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> contracts, Action<StreamDataEvent<FuturesStreamPerpetualTicker>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToTickersAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);

    /*
    public async Task<CallResult<GateStreamResponse<GateStreamStatus>>> UnsubscribeFromTickersAsync(IEnumerable<string> contracts, Action<StreamDataEvent<GateStreamResponse<GateStreamStatus>>> onMessage, CancellationToken ct = default)
        => await MainClient.UnsubscribeFromTickersAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);
    */

    public async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(IEnumerable<string> contracts, Action<StreamDataEvent<FuturesStreamTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToTradesAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookTickersAsync(IEnumerable<string> contracts, Action<StreamDataEvent<FuturesStreamBookTicker>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToOrderBookTickersAsync(this.Settlement, contracts, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string contract, int frequency, int level, Action<StreamDataEvent<FuturesStreamBookDifference>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToOrderBookDifferencesAsync(this.Settlement, contract, frequency, level, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(string contract, /*int interval,*/ int limit, Action<StreamDataEvent<FuturesStreamBookSnapshot>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToOrderBookSnapshotsAsync(this.Settlement, contract, /*int interval,*/  limit, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(string contract, FuturesCandlestickInterval interval, Action<StreamDataEvent<FuturesStreamCandlestick>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToCandlesticksAsync(this.Settlement, "mark_", contract, interval, onMessage, ct).ConfigureAwait(false);

    //public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceCandlesticksAsync(string contract, FuturesCandlestickInterval interval, Action<StreamDataEvent<FuturesStreamCandlestick>> onMessage, CancellationToken ct = default)
    //    => await MainClient.SubscribeToCandlesticksAsync(this.Settlement, "index_", contract, interval, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserOrdersAsync(int userId, Action<StreamDataEvent<FuturesOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserOrdersAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserOrdersAsync(int userId, string contract, Action<StreamDataEvent<FuturesOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserOrdersAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradesAsync(int userId, Action<StreamDataEvent<FuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserTradesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradesAsync(int userId, string contract, Action<StreamDataEvent<FuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserTradesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidatesAsync(int userId, Action<StreamDataEvent<FuturesUserLiquidate>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserLiquidatesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidatesAsync(int userId, string contract, Action<StreamDataEvent<FuturesUserLiquidate>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserLiquidatesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserDeleveragesAsync(int userId, Action<StreamDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserDeleveragesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserDeleveragesAsync(int userId, string contract, Action<StreamDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserDeleveragesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionClosesAsync(int userId, Action<StreamDataEvent<FuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionClosesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionClosesAsync(int userId, string contract, Action<StreamDataEvent<FuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionClosesAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserBalancesAsync(int userId, Action<StreamDataEvent<FuturesStreamBalance>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserBalancesAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(int userId, Action<StreamDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserReduceRiskLimitsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(int userId, string contract, Action<StreamDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserReduceRiskLimitsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionsAsync(int userId, Action<StreamDataEvent<FuturesPosition>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionsAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionsAsync(int userId, string contract, Action<StreamDataEvent<FuturesPosition>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserPositionsAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

    public async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoOrdersAsync(int userId, Action<StreamDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserAutoOrdersAsync(this.Settlement, userId, onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoOrdersAsync(int userId, string contract, Action<StreamDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
        => await MainClient.SubscribeToUserAutoOrdersAsync(this.Settlement, userId, contract, onMessage, ct).ConfigureAwait(false);

}