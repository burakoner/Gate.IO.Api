using Gate.IO.Api.Clients.StreamApi;
using Gate.IO.Api.Models.StreamApi;

namespace Gate.IO.Api;

public class GateStreamClient
{
    // Options
    public GateStreamClientOptions ClientOptions { get; }

    // Master Clients
    public StreamApiBaseClient Base { get; }
    public StreamApiSpotClient Spot { get; }
    public StreamApiFuturesClient Futures { get; }

    public GateStreamClient() : this(new GateStreamClientOptions())
    {
    }

    public GateStreamClient(GateStreamClientOptions options)
    {
        ClientOptions = options;

        Base = new StreamApiBaseClient(this);
        Spot = new StreamApiSpotClient(this);
        Futures = new StreamApiFuturesClient(this);
    }

}
