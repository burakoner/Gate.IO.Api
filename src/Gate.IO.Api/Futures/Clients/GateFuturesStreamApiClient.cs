namespace Gate.IO.Api.Futures;

public class GateFuturesStreamApiClient
{
    // Public
    public GateFuturesStreamApiSettleClient BTC { get; }
    public GateFuturesStreamApiSettleClient USD { get; }
    public GateFuturesStreamApiSettleClient USDT { get; }

    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal StreamApiBaseClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }

    // Channels
    private const string futuresPingChannel = "futures.ping";
    private const string futuresTickersChannel = "futures.tickers";
    private const string futuresTradesChannel = "futures.trades";
    private const string futuresOrderBookChannel = "futures.order_book";
    private const string futuresBookTickerChannel = "futures.book_ticker";
    private const string futuresOrderBookUpdateChannel = "futures.order_book_update";
    private const string futuresCandlesticksChannel = "futures.candlesticks";
    private const string futuresUserOrdersChannel = "futures.orders";
    private const string futuresUserTradesChannel = "futures.usertrades";
    private const string futuresUserLiquidatesChannel = "futures.liquidates";
    private const string futuresUserDeleveragesChannel = "futures.auto_deleverages";
    private const string futuresUserPositionClosesChannel = "futures.position_closes";
    private const string futuresUserBalancesChannel = "futures.balances";
    private const string futuresUserReduceRiskLimitsChannel = "futures.reduce_risk_limits";
    private const string futuresUserPositionsChannel = "futures.positions";
    private const string futuresUserAutoOrdersChannel = "futures.autoorders";

    internal GateFuturesStreamApiClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;

        BTC = new GateFuturesStreamApiSettleClient(this, GateFuturesSettlement.BTC);
        USDT = new GateFuturesStreamApiSettleClient(this, GateFuturesSettlement.USDT);
    }

    internal async Task UnsubscribeAsync(int subscriptionId)
        => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    internal async Task UnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    internal async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);

    internal async Task<CallResult<GateStreamLatency>> PingAsync(GateFuturesSettlement settle)
        => await BaseClient.PingAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresPingChannel).ConfigureAwait(false);

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(GateFuturesSettlement settle, IEnumerable<string> contracts, Action<WebSocketDataEvent<GateFuturesStreamPerpetualTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesStreamPerpetualTicker>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresTickersChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    /*
    internal async Task<CallResult<GateStreamResponse<GateStreamStatus>>> UnsubscribeFromTickersAsync(FuturesPerpetualSettle settle, IEnumerable<string> contracts, Action<WebSocketDataEvent<GateStreamResponse<GateStreamStatus>>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateStreamStatus>>>(data => onMessage(data));
        return await BaseClient.BaseUnsubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresTickersChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }
    */

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(GateFuturesSettlement settle, IEnumerable<string> contracts, Action<WebSocketDataEvent<GateFuturesStreamTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesStreamTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresTradesChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookTickersAsync(GateFuturesSettlement settle, IEnumerable<string> contracts, Action<WebSocketDataEvent<GateFuturesStreamBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateFuturesStreamBookTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresBookTickerChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(GateFuturesSettlement settle, string contract, int frequency, int level, Action<WebSocketDataEvent<GateFuturesStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 5, 10, 20, 50, 100);
        frequency.ValidateIntValues(nameof(frequency), 100, 1000);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add($"{frequency}ms");
        payload.Add(level.ToString());

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateFuturesStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(GateFuturesSettlement settle, string contract, /*int interval,*/ int limit, Action<WebSocketDataEvent<GateFuturesStreamBookSnapshot>> onMessage, CancellationToken ct = default)
    {
        limit.ValidateIntValues(nameof(limit), 1, 5, 10, 20, 50, 100);
        // interval.ValidateIntValues(nameof(interval), 100, 1000);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add(limit.ToString());
        payload.Add($"0");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateFuturesStreamBookSnapshot>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresOrderBookChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(GateFuturesSettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, Action<WebSocketDataEvent<FuturesStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(prefix + contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<FuturesStreamCandlestick>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesOrder>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserOrdersAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesOrder>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesOrder>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserOrdersChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserTradesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesUserTrade>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesUserTrade>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserTradesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidationsAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesUserLiquidation>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserLiquidationsAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidationsAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesUserLiquidation>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesUserLiquidation>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserLiquidatesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserDeleveragesAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserDeleveragesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserDeleveragesAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesUserDeleverage>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesUserDeleverage>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserDeleveragesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserPositionClosesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesPositionClose>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesPositionClose>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserPositionClosesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserBalancesAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesStreamBalance>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesStreamBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserBalancesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
    => await SubscribeToUserReduceRiskLimitsAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesStreamReduceRiskLimit>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserReduceRiskLimitsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesPosition>> onMessage, CancellationToken ct = default)
    => await SubscribeToUserPositionsAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesPosition>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesPosition>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserPositionsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAutoOrdersAsync(GateFuturesSettlement settle, long userId, Action<WebSocketDataEvent<GateFuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
    => await SubscribeToUserAutoOrdersAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAutoOrdersAsync(GateFuturesSettlement settle, long userId, string contract, Action<WebSocketDataEvent<GateFuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateFuturesStreamAutoOrder>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamPerpetualFuturesAddresses[settle], futuresUserAutoOrdersChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

}