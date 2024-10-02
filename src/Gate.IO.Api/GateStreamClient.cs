using Gate.IO.Api.Authentication;
using Gate.IO.Api.Clients.RestApi;
using Gate.IO.Api.Clients.StreamApi;
using Gate.IO.Api.Converters;
using Gate.IO.Api.Enums;
using Gate.IO.Api.Extensions;
using Gate.IO.Api.Helpers;
using Gate.IO.Api.Models.StreamApi;
using Gate.IO.Api.Spot;
using Gate.IO.Api.SubAccount;
using Gate.IO.Api.Wallet;

namespace Gate.IO.Api;

public class GateStreamClient
{
    // Options
    internal ILogger Logger { get; }
    public GateStreamClientOptions ClientOptions { get; }

    // Master Clients
    internal StreamApiBaseClient Base { get; }
    public StreamApiSpotClient Spot { get; }
    public StreamApiFuturesClient Futures { get; }
    public StreamApiOptionsClient Options { get; }

    public GateStreamClient() : this(null, new GateStreamClientOptions())
    {
    }

    public GateStreamClient(ILogger logger) : this(logger, new GateStreamClientOptions())
    {
    }

    public GateStreamClient(GateStreamClientOptions options) : this(null, options)
    {
    }

    public GateStreamClient(ILogger logger, GateStreamClientOptions options)
    {
        Logger = logger;
        ClientOptions = options;

        Base = new StreamApiBaseClient(this);
        Spot = new StreamApiSpotClient(this);
        Futures = new StreamApiFuturesClient(this);
        Options = new StreamApiOptionsClient(this);
    }

}
