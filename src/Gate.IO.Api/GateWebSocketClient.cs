namespace Gate.IO.Api;

/// <summary>
/// Represents the Gate Web Socket Client.
/// </summary>
public class GateWebSocketClient
{
    // Options
    internal ILogger Logger { get; }
    /// <summary>
    /// Gets or sets the Client Options.
    /// </summary>
    public GateWebSocketClientOptions ClientOptions { get; }

    // Master Clients
    internal GateBaseStreamApiClient Base { get; }
    /// <summary>
    /// Gets or sets the Spot.
    /// </summary>
    public GateSpotStreamApiClient Spot { get; }
    /// <summary>
    /// Gets or sets the Futures.
    /// </summary>
    public GateFuturesStreamApiClient Futures { get; }
    /// <summary>
    /// Gets or sets the Delivery.
    /// </summary>
    public GateDeliveryStreamApiClient Delivery { get; }
    /// <summary>
    /// Gets or sets the Options.
    /// </summary>
    public GateOptionsStreamApiClient Options { get; }
    /// <summary>
    /// Gets or sets the TradFi.
    /// </summary>
    public GateTradFiStreamApiClient TradFi { get; }
    /// <summary>
    /// Gets or sets the Unified.
    /// </summary>
    public GateUnifiedStreamApiClient Unified { get; }
    /// <summary>
    /// Gets or sets the CrossEx.
    /// </summary>
    public GateCrossExStreamApiClient CrossEx { get; }
    /// <summary>
    /// Gets or sets the Announcements.
    /// </summary>
    public GateAnnouncementsStreamApiClient Announcements { get; }

    /// <summary>
    /// Initializes a new instance of the Gate Web Socket Client class.
    /// </summary>
    public GateWebSocketClient() : this(null, new GateWebSocketClientOptions())
    {
    }

    /// <summary>
    /// Initializes a new instance of the Gate Web Socket Client class.
    /// </summary>
    public GateWebSocketClient(ILogger logger) : this(logger, new GateWebSocketClientOptions())
    {
    }

    /// <summary>
    /// Initializes a new instance of the Gate Web Socket Client class.
    /// </summary>
    public GateWebSocketClient(GateWebSocketClientOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the Gate Web Socket Client class.
    /// </summary>
    public GateWebSocketClient(ILogger logger, GateWebSocketClientOptions options)
    {
        Logger = logger;
        ClientOptions = options;

        Base = new GateBaseStreamApiClient(this);
        Spot = new GateSpotStreamApiClient(this);
        Futures = new GateFuturesStreamApiClient(this);
        Delivery = new GateDeliveryStreamApiClient(this);
        Options = new GateOptionsStreamApiClient(this);
        TradFi = new GateTradFiStreamApiClient(this);
        Unified = new GateUnifiedStreamApiClient(this);
        CrossEx = new GateCrossExStreamApiClient(this);
        Announcements = new GateAnnouncementsStreamApiClient(this);
    }

}
