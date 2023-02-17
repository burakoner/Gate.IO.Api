using Gate.IO.Api.Clients.StreamApi;

namespace Gate.IO.Api;

public class GateStreamClient
{
    // Options
    public GateStreamClientOptions ClientOptions { get; }

    // Master Clients
    public StreamSpotClient Spot { get; }

    public GateStreamClient() : this(new GateStreamClientOptions())
    {
    }

    public GateStreamClient(GateStreamClientOptions options)
    {
        ClientOptions = options;

        Spot = new StreamSpotClient(this);
    }
}
