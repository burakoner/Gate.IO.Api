﻿using Gate.IO.Api.Spot;
using Gate.IO.Api.SubAccount;
using Gate.IO.Api.Wallet;

namespace Gate.IO.Api;

/// <summary>
/// Gate.IO REST API Client
/// </summary>
public sealed class GateRestApiClient : RestApiClient
{
    // Options
    internal ILogger Logger { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// Wallet Client
    /// </summary>
    public GateWalletRestApiClient Wallet { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public GateSubAccountRestApiClient SubAccount { get; }

    // TODO: Unified

    /// <summary>
    /// Spot Client
    /// </summary>
    public GateSpotRestApiClient Spot { get; }

    public RestApiMarginClient Margin { get; }
    public RestApiFuturesClient Futures { get; }
    public RestApiSwapClient Swap { get; }
    public RestApiOptionsClient Options { get; }
    public RestApiRebateClient Rebate { get; }
    public RestApiBrokerClient Broker { get; }

    public GateRestApiClient() : this(null, new GateRestApiClientOptions())
    {
    }

    public GateRestApiClient(ILogger logger) : this(logger, new GateRestApiClientOptions())
    {
    }

    public GateRestApiClient(GateRestApiClientOptions options) : this(null, options)
    {
    }

    public GateRestApiClient(ILogger logger, GateRestApiClientOptions options):base(logger, options)
    {
        Logger = logger;
        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Wallet = new GateWalletRestApiClient(this);
        SubAccount = new GateSubAccountRestApiClient(this);
        Spot = new GateSpotRestApiClient(this);
        Margin = new RestApiMarginClient(this);
        Futures = new RestApiFuturesClient(this);
        Swap = new RestApiSwapClient(this);
        Options = new RestApiOptionsClient(this);
        Rebate = new RestApiRebateClient(this);
        Broker = new RestApiBrokerClient(this);
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
        return new Uri(ClientOptions.BaseAddress.AppendPath(api).AppendPath($"v{version}").AppendPath(section).AppendPath(endpoint));
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