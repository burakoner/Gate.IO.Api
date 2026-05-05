namespace Gate.IO.Api.Announcements;

/// <summary>
/// Gate.IO Announcements WebSocket API client.
/// </summary>
public class GateAnnouncementsStreamApiClient
{
    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal GateBaseStreamApiClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    internal string BaseAddress { get; }

    // Channels
    private const string announcementPingChannel = "announcement.ping";

    internal GateAnnouncementsStreamApiClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
        BaseAddress = root.ClientOptions.StreamAnnouncementsAddress;
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
    /// Executes the announcement service liveness check.
    /// </summary>
    public async Task<CallResult<GateStreamLatency>> PingAsync()
        => await BaseClient.PingAsync(BaseAddress, announcementPingChannel).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to listing announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToListingAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.Listing, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to delisting announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToDelistingAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.Delisting, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to fee announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToFeeAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.Fee, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to ETF announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToEtfAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.Etf, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to deposit and withdrawal announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToDepositWithdrawalAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.DepositWithdrawal, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to rename announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToRenameAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.Rename, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to precision announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPrecisionAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.Precision, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to engine upgrade announcement summaries.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToEngineUpgradeAnnouncementsAsync(IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
        => await SubscribeToAnnouncementsAsync(GateAnnouncementType.EngineUpgrade, languages, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to announcement summaries for the specified announcement channel type.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAnnouncementsAsync(GateAnnouncementType type, IEnumerable<GateAnnouncementLanguage> languages, Action<WebSocketDataEvent<GateAnnouncementStreamSummary>> onMessage, CancellationToken ct = default)
    {
        var channel = MapConverter.GetString(type);
        var payload = CreateLanguagePayload(languages);
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateAnnouncementStreamSummary>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.AnnouncementSubscribeAsync(BaseAddress, channel, payload, handler, ct).ConfigureAwait(false);
    }

    private static List<string> CreateLanguagePayload(IEnumerable<GateAnnouncementLanguage> languages)
    {
        if (languages == null)
            throw new ArgumentNullException(nameof(languages));

        var payload = languages.Select(MapConverter.GetString).ToList();
        if (payload.Count == 0)
            throw new ArgumentException("At least one announcement language is required.", nameof(languages));

        return payload;
    }
}
