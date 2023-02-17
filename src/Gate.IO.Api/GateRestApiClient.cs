namespace Gate.IO.Api;

public sealed class GateRestApiClient
{
    // Options
    internal GateRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public WalletRestApiClient Wallet { get; }
    public SubAccountRestApiClient SubAccount { get; }
    public SpotRestApiClient Spot { get; }
    public MarginRestApiClient Margin { get; }
    public FuturesRestApiClient Futures { get; }
    public FlashSwapRestApiClient FlashSwap { get; }
    public OptionsRestApiClient Options { get; }
    public RebateRestApiClient Rebate { get; }

    public GateRestApiClient() : this(new GateRestApiClientOptions())
    {
    }

    public GateRestApiClient(GateRestApiClientOptions options)
    {
        ClientOptions = options;

        Wallet = new WalletRestApiClient(this);
        SubAccount = new SubAccountRestApiClient(this);
        Spot = new SpotRestApiClient(this);
        Margin = new MarginRestApiClient(this);
        Futures = new FuturesRestApiClient(this);
        FlashSwap = new FlashSwapRestApiClient(this);
        Options = new OptionsRestApiClient(this);
        Rebate = new RebateRestApiClient(this);
    }

    internal Uri GetUrl(string api, string version, string section, string endpoint)
    {
        return new Uri(ClientOptions.BaseAddress.AppendPath(api).AppendPath($"v{version}").AppendPath(section).AppendPath(endpoint));
    }

    internal CallError ParseErrorResponse(JToken error)
    {
        if (!error.HasValues)
            return new ServerError(error.ToString());

        if (error["message"] == null && error["label"] == null)
            return new ServerError(error.ToString());

        if (error["message"] != null && error["label"] == null)
            return new ServerError((string)error["message"]!);

        if (error["message"] == null && error["label"] != null)
            return new ServerError((string)error["label"]!);

        return new ServerError(0, (string)error["message"]!, (string)error["label"]!);
    }
}
