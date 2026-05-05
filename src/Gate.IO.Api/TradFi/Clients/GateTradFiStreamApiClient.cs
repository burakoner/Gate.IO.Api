namespace Gate.IO.Api.TradFi;

/// <summary>
/// Gate.IO TradFi WebSocket API Client.
/// </summary>
public class GateTradFiStreamApiClient
{
    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal GateBaseStreamApiClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    internal string BaseAddress { get; }

    // Channels
    private const string tradFiPingChannel = "tradfi.ping";
    private const string tradFiTickersChannel = "tradfi.tickers";
    private const string tradFiCandlesticksChannel = "tradfi.candlesticks";
    private const string tradFiOrderBookChannel = "tradfi.order_book";
    private const string tradFiOrdersChannel = "tradfi.orders";
    private const string tradFiPositionChannel = "tradfi.position";
    private const string tradFiBalanceChannel = "tradfi.balance";

    internal GateTradFiStreamApiClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
        BaseAddress = root.ClientOptions.StreamTradFiAddress;
    }

    /// <summary>
    /// Executes the Unsubscribe operation.
    /// </summary>
    public async Task UnsubscribeAsync(int subscriptionId)
        => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    /// <summary>
    /// Executes the Unsubscribe operation.
    /// </summary>
    public async Task UnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    /// <summary>
    /// Executes the Unsubscribe All operation.
    /// </summary>
    public async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);

    /// <summary>
    /// Executes the Ping operation.
    /// </summary>
    public async Task<CallResult<GateStreamLatency>> PingAsync()
        => await BaseClient.PingAsync(BaseAddress, tradFiPingChannel).ConfigureAwait(false);

    /// <summary>
    /// Executes the Subscribe To Tickers operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateTradFiStreamTicker>> onMessage, CancellationToken ct = default)
    {
        var payload = new { markets = symbols.ToList() };
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateTradFiStreamTicker>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, tradFiTickersChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Candlesticks operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(string symbol, GateTradFiKlineInterval interval, Action<WebSocketDataEvent<GateTradFiStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(symbol);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateTradFiStreamCandlestick>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, tradFiCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateTradFiStreamOrderBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateTradFiStreamOrderBookTicker>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, tradFiOrderBookChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Orders operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(Action<WebSocketDataEvent<GateTradFiStreamOrder>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateTradFiStreamOrder>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, tradFiOrdersChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Positions operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(Action<WebSocketDataEvent<GateTradFiStreamPosition>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateTradFiStreamPosition>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, tradFiPositionChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Balances operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserBalancesAsync(Action<WebSocketDataEvent<GateTradFiStreamBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateTradFiStreamBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, tradFiBalanceChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }
}
