namespace Gate.IO.Api;

public sealed class GateRestApiClient
{
    // Options
    internal GateRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public RestApiWalletClient Wallet { get; }
    public RestApiSubAccountClient SubAccount { get; }
    public RestApiSpotClient Spot { get; }
    public RestApiMarginClient Margin { get; }
    public RestApiFuturesClient Futures { get; }
    public RestApiFlashSwapClient FlashSwap { get; }
    public RestApiOptionsClient Options { get; }
    public RestApiRebateClient Rebate { get; }

    public GateRestApiClient() : this(new GateRestApiClientOptions())
    {
    }

    public GateRestApiClient(GateRestApiClientOptions options)
    {
        ClientOptions = options;

        Wallet = new RestApiWalletClient(this);
        SubAccount = new RestApiSubAccountClient(this);
        Spot = new RestApiSpotClient(this);
        Margin = new RestApiMarginClient(this);
        Futures = new RestApiFuturesClient(this);
        FlashSwap = new RestApiFlashSwapClient(this);
        Options = new RestApiOptionsClient(this);
        Rebate = new RestApiRebateClient(this);
    }

    internal Uri GetUrl(string api, string version, string section, string endpoint)
    {
        return new Uri(ClientOptions.BaseAddress.AppendPath(api).AppendPath($"v{version}").AppendPath(section).AppendPath(endpoint));
    }

    internal Error ParseErrorResponse(JToken error)
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
