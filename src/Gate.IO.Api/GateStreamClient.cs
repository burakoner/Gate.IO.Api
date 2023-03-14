namespace Gate.IO.Api;

public class GateStreamClient
{
    // Options
    public GateStreamClientOptions ClientOptions { get; }

    // Master Clients
    internal StreamApiBaseClient Base { get; }
    public StreamApiSpotClient Spot { get; }
    public StreamApiFuturesClient Futures { get; }
    public StreamApiOptionsClient Options { get; }

    public GateStreamClient() : this(new GateStreamClientOptions())
    {
    }

    public GateStreamClient(GateStreamClientOptions options)
    {
        ClientOptions = options;

        Base = new StreamApiBaseClient(this);
        Spot = new StreamApiSpotClient(this);
        Futures = new StreamApiFuturesClient(this);
        Options = new StreamApiOptionsClient(this);
    }

}
