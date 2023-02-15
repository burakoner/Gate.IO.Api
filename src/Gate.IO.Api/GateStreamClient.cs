using Gate.IO.Api.Clients.StreamApi;

namespace Gate.IO.Api;

public class GateStreamClient
{
    // Options
    public GateStreamClientOptions Options { get; }

    // Master Clients
    public StreamSpotClient Spot { get; }

    public GateStreamClient() : this(new GateStreamClientOptions())
    {
    }

    public GateStreamClient(GateStreamClientOptions options)
    {
        Options = options;

        Spot = new StreamSpotClient(this);
    }
}
