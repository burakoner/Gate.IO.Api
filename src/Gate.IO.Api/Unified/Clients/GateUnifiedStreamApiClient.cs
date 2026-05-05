namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate.IO Unified WebSocket API Client.
/// </summary>
public class GateUnifiedStreamApiClient
{
    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal GateBaseStreamApiClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    internal string BaseAddress { get; }

    // Channels
    private const string unifiedPingChannel = "unified.ping";
    private const string unifiedAssetsChannel = "unified.assets";
    private const string unifiedAssetDetailChannel = "unified.asset_detail";

    internal GateUnifiedStreamApiClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
        BaseAddress = root.ClientOptions.StreamUnifiedAddress;
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
        => await BaseClient.PingAsync(BaseAddress, unifiedPingChannel).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to the Unified asset overview stream.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAssetsAsync(Action<WebSocketDataEvent<GateUnifiedStreamAssets>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateUnifiedStreamAssets>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, unifiedAssetsChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Subscribes to the Unified asset detail stream for all currencies.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAllAssetDetailsAsync(Action<WebSocketDataEvent<GateUnifiedStreamAssetDetail>> onMessage, CancellationToken ct = default)
        => await SubscribeToAssetDetailsAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to the Unified asset detail stream.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAssetDetailsAsync(IEnumerable<string> currencies, Action<WebSocketDataEvent<GateUnifiedStreamAssetDetail>> onMessage, CancellationToken ct = default)
    {
        var payload = currencies.ToList();
        if (payload.Contains("!all") && payload.Count > 1)
            throw new ArgumentException("The !all marker cannot be mixed with individual currencies.", nameof(currencies));

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateUnifiedStreamAssetDetail>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, unifiedAssetDetailChannel, payload, true, handler, ct).ConfigureAwait(false);
    }
}
