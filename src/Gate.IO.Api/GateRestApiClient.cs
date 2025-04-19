using Gate.IO.Api.Rebate;

namespace Gate.IO.Api;

/// <summary>
/// Gate.IO REST API Client
/// </summary>
public class GateRestApiClient : RestApiClient
{
    // Options
    internal ILogger Logger { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// Wallet Client
    /// </summary>
    public GateWalletRestApiClient Wallet { get; } // 418

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public GateSubAccountRestApiClient SubAccount { get; } // 419

    // TODO: Unified

    /// <summary>
    /// Spot Client
    /// </summary>
    public GateSpotRestApiClient Spot { get; } // 419

    /// <summary>
    /// Margin Client
    /// </summary>
    public GateMarginRestApiClient Margin { get; } // 419

    // TODO: MarginUni

    /// <summary>
    /// Flash Swap Client
    /// </summary>
    public GateSwapRestApiClient FlashSwap { get; } // 419

    /// <summary>
    /// Futures Client
    /// </summary>
    public GateFuturesRestApiClient Futures { get; }

    /// <summary>
    /// Options Client
    /// </summary>
    public GateOptionsRestApiClient Options { get; }

    // TODO: EarnUni
    // TODO: Collateral-loan
    // TODO: Multi-collateral-loan
    // TODO: Earn
    // TODO: Account

    /// <summary>
    /// Rebate Client
    /// </summary>
    public GateRebateRestApiClient Rebate { get; }

    /// <summary>
    /// Gate.IO REST API Client Constructor
    /// </summary>
    public GateRestApiClient() : this(null, new GateRestApiClientOptions())
    {
    }

    /// <summary>
    /// Gate.IO REST API Client Constructor
    /// </summary>
    /// <param name="logger">ILogger Instance</param>
    public GateRestApiClient(ILogger logger) : this(logger, new GateRestApiClientOptions())
    {
    }

    /// <summary>
    /// Gate.IO REST API Client Constructor
    /// </summary>
    /// <param name="options">GateRestApiClientOptions Instance</param>
    public GateRestApiClient(GateRestApiClientOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// Gate.IO REST API Client Constructor
    /// </summary>
    /// <param name="logger">ILogger Instance</param>
    /// <param name="options">GateRestApiClientOptions Instance</param>
    public GateRestApiClient(ILogger logger, GateRestApiClientOptions options) : base(logger, options)
    {
        Logger = logger;
        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Wallet = new GateWalletRestApiClient(this);
        SubAccount = new GateSubAccountRestApiClient(this);
        Spot = new GateSpotRestApiClient(this);
        Margin = new GateMarginRestApiClient(this);
        Futures = new GateFuturesRestApiClient(this);
        FlashSwap = new GateSwapRestApiClient(this);
        Options = new GateOptionsRestApiClient(this);
        Rebate = new GateRebateRestApiClient(this);
    }

    #region Override Methods
    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => new GateAuthenticationProvider(credentials);

    /// <inheritdoc />
    protected override Error ParseErrorResponse(JToken error)
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
    #endregion

    #region Internal Methods
    internal Uri GetUrl(string api, string version, string section, string endpoint)
    {
        var url = ClientOptions.BaseAddress;
        if (!string.IsNullOrEmpty(api)) url = url.AppendPath(api);
        if (!string.IsNullOrEmpty(version)) url = url.AppendPath($"v{version}");
        if (!string.IsNullOrEmpty(section)) url = url.AppendPath(section);
        if (!string.IsNullOrEmpty(endpoint)) url = url.AppendPath(endpoint);

        return new Uri(url);
    }

    internal async Task<RestCallResult<T>> SendRequestInternal<T>(
        Uri uri,
        HttpMethod method,
        CancellationToken cancellationToken,
        bool signed = false,
        Dictionary<string, object> queryParameters = null,
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null,
        ArraySerialization? arraySerialization = null,
        JsonSerializer deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }

    internal string CheckOrderId(long? orderId, string clientOrderId)
    {
        if (orderId == null && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or clientOrderId must be provided");
        if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or clientOrderId must be provided, not both");

        return orderId != null ? orderId.ToString() : clientOrderId;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="apiKey">API Key</param>
    /// <param name="apiSecret">API Secret</param>
    public void SetApiCredentials(string apiKey, string apiSecret)
    {
        base.SetApiCredentials(new ApiCredentials(apiKey, apiSecret));
    }
    #endregion
}