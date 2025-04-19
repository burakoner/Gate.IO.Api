namespace Gate.IO.Api;

public class GateWebSocketClient
{
    // Options
    internal ILogger Logger { get; }
    public GateWebSocketClientOptions ClientOptions { get; }

    // Master Clients
    internal StreamApiBaseClient Base { get; }
    public StreamApiSpotClient Spot { get; }
    public GateFuturesStreamApiClient Futures { get; }
    public GateDeliveryStreamApiClient Delivery { get; }
    public StreamApiOptionsClient Options { get; }

    public GateWebSocketClient() : this(null, new GateWebSocketClientOptions())
    {
    }

    public GateWebSocketClient(ILogger logger) : this(logger, new GateWebSocketClientOptions())
    {
    }

    public GateWebSocketClient(GateWebSocketClientOptions options) : this(null, options)
    {
    }

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
