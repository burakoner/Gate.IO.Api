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
    internal StreamApiBaseClient Base { get; }
    /// <summary>
    /// Gets or sets the Spot.
    /// </summary>
    public StreamApiSpotClient Spot { get; }
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
    public StreamApiOptionsClient Options { get; }

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

        Base = new StreamApiBaseClient(this);
        Spot = new StreamApiSpotClient(this);
        Futures = new GateFuturesStreamApiClient(this);
        Delivery = new GateDeliveryStreamApiClient(this);
        Options = new StreamApiOptionsClient(this);
    }

}
