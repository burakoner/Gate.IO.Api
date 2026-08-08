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
    public GateWalletRestApiClient Wallet { get; }

    /// <summary>
    /// Withdrawal Client
    /// Alias for withdrawal endpoints under Wallet
    /// </summary>
    public GateWalletRestApiClient Withdrawal => Wallet;

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public GateSubAccountRestApiClient SubAccount { get; }

    /// <summary>
    /// Unified Client
    /// </summary>
    public GateUnifiedRestApiClient Unified { get; }

    /// <summary>
    /// Margin Client
    /// </summary>
    public GateMarginRestApiClient IsolatedMargin { get; }

    /// <summary>
    /// Spot Client
    /// </summary>
    public GateSpotRestApiClient Spot { get; }

    /// <summary>
    /// Flash Swap Client
    /// </summary>
    public GateSwapRestApiClient FlashSwap { get; }

    /// <summary>
    /// Perpetual Futures Client
    /// </summary>
    public GateFuturesRestApiClient Futures { get; }

    /// <summary>
    /// Delivery Futures Client
    /// </summary>
    public GateDeliveryRestApiClient Delivery { get; }

    /// <summary>
    /// TradFi Client
    /// </summary>
    public GateTradFiRestApiClient TradFi { get; }

    /// <summary>
    /// Options Client
    /// </summary>
    public GateOptionsRestApiClient Options { get; }

    /// <summary>
    /// EarnUni Client
    /// </summary>
    public GateEarnUniRestApiClient EarnUni { get; }

    // TODO: Collateral-loan

    /// <summary>
    /// Multi-Collateral Loan Client
    /// </summary>
    public GateMultiCollateralLoanRestApiClient MultiCollateralLoan { get; }

    /// <summary>
    /// Earn Client
    /// </summary>
    public GateEarnRestApiClient Earn { get; }

    /// <summary>
    /// Account Client
    /// </summary>
    public GateAccountRestApiClient Account { get; }

    /// <summary>
    /// Rebate Client
    /// </summary>
    public GateRebateRestApiClient Rebate { get; }

    /// <summary>
    /// OTC Client
    /// </summary>
    public GateOtcRestApiClient Otc { get; }

    /// <summary>
    /// P2P Client
    /// </summary>
    public GateP2pRestApiClient P2p { get; }

    /// <summary>
    /// CrossEx Client
    /// </summary>
    public GateCrossExRestApiClient CrossEx { get; }

    /// <summary>
    /// Alpha Client
    /// </summary>
    public GateAlphaRestApiClient Alpha { get; }

    /// <summary>
    /// Bot Client
    /// </summary>
    public GateBotRestApiClient Bot { get; }

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
    public GateRestApiClient(ILogger logger, GateRestApiClientOptions options) : base(logger, options ??= new GateRestApiClientOptions())
    {
        Logger = logger;
        RequestFactory = new GateRequestFactory();
        RequestFactory.Configure(options.HttpOptions, options.Proxy, options.HttpClient);
        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Wallet = new GateWalletRestApiClient(this);
        SubAccount = new GateSubAccountRestApiClient(this);
        Unified = new GateUnifiedRestApiClient(this);
        IsolatedMargin = new GateMarginRestApiClient(this);
        Spot = new GateSpotRestApiClient(this);
        FlashSwap = new GateSwapRestApiClient(this);
        Futures = new GateFuturesRestApiClient(this);
        Delivery = new GateDeliveryRestApiClient(this);
        TradFi = new GateTradFiRestApiClient(this);

        Options = new GateOptionsRestApiClient(this);
        EarnUni = new GateEarnUniRestApiClient(this);
        MultiCollateralLoan = new GateMultiCollateralLoanRestApiClient(this);
        Earn = new GateEarnRestApiClient(this);
        Account = new GateAccountRestApiClient(this);
        Rebate = new GateRebateRestApiClient(this);
        Otc = new GateOtcRestApiClient(this);
        P2p = new GateP2pRestApiClient(this);
        CrossEx = new GateCrossExRestApiClient(this);
        Alpha = new GateAlphaRestApiClient(this);
        Bot = new GateBotRestApiClient(this);
    }

    #region Override Methods
    /// <inheritdoc />
    protected override string PrepareBodyContent(SortedDictionary<string, object> parameters, RestRequestBodyFormat format)
    {
        var multipart = GateMultipartFormData.Find(parameters);
        return multipart?.Body ?? base.PrepareBodyContent(parameters, format);
    }

    /// <inheritdoc />
    protected override ApiSharp.Interfaces.IRequest ConstructRequest(
        Uri uri,
        HttpMethod method,
        bool signed,
        Dictionary<string, object> queryParameters,
        Dictionary<string, object> bodyParameters,
        Dictionary<string, string> headerParameters,
        ArraySerialization serialization,
        int requestId)
    {
        var multipart = GateMultipartFormData.Find(bodyParameters);
        var request = base.ConstructRequest(uri, method, signed, queryParameters, bodyParameters, headerParameters, serialization, requestId);

        if (multipart != null)
            request.SetContent(multipart.Body, multipart.ContentType);

        return request;
    }

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => new GateAuthentication(credentials);

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

        var endpoint = uri.AbsolutePath;
        var stopwatch = Stopwatch.StartNew();
        Logger?.LogDebug(
            "Gate REST request started: {Method} {Endpoint}. Signed={Signed}; QueryParameters={QueryParameterCount}; BodyParameters={BodyParameterCount}; ResponseType={ResponseType}",
            method.Method,
            endpoint,
            signed,
            queryParameters?.Count ?? 0,
            bodyParameters?.Count ?? 0,
            typeof(T).Name);

        try
        {
            var result = await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
            stopwatch.Stop();

            if (result.Success)
            {
                Logger?.LogDebug(
                    "Gate REST request succeeded: {Method} {Endpoint} in {ElapsedMilliseconds}ms. ResponseType={ResponseType}",
                    method.Method,
                    endpoint,
                    stopwatch.ElapsedMilliseconds,
                    typeof(T).Name);
            }
            else
            {
                Logger?.LogWarning(
                    "Gate REST request failed: {Method} {Endpoint} in {ElapsedMilliseconds}ms. Error={Error}",
                    method.Method,
                    endpoint,
                    stopwatch.ElapsedMilliseconds,
                    result.Error);
            }

            return result;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Logger?.LogError(
                ex,
                "Gate REST request threw an exception: {Method} {Endpoint} after {ElapsedMilliseconds}ms",
                method.Method,
                endpoint,
                stopwatch.ElapsedMilliseconds);
            throw;
        }
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
