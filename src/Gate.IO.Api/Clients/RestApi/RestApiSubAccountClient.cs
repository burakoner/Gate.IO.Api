using Gate.IO.Api.Models.RestApi.SubAccount;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiSubAccountClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string subaccounts = "sub_accounts";

    // Endpoints
    private const string keysEndpoint = "keys";
    private const string lockEndpoint = "lock";
    private const string unlockEndpoint = "unlock";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO SubAccount RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal RestApiSubAccountClient(GateRestApiClient root) : base("Gate.IO SubAccount RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new GateAuthenticationProvider(credentials);

    protected override Error ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    protected override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
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

    #region Create a new sub-account
    public async Task<RestCallResult<SubAccount>> CreateSubAccountAsync(
        string login,
        string password = "",
        string email = "",
        string remark = "",
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "login_name", login },
        };
        parameters.AddOptionalParameter("email", email);
        parameters.AddOptionalParameter("password", password);
        parameters.AddOptionalParameter("remark", remark);

        return await SendRequestInternal<SubAccount>(RootClient.GetUrl(api, version, subaccounts, ""), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List sub-accounts
    public async Task<RestCallResult<IEnumerable<SubAccount>>> GetSubAccountsAsync(CancellationToken ct = default)
    {
        return await SendRequestInternal<IEnumerable<SubAccount>>(RootClient.GetUrl(api, version, subaccounts, ""), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Get the sub-account
    public async Task<RestCallResult<SubAccount>> GetSubAccountsAsync(long subAccountId, CancellationToken ct = default)
    {
        return await SendRequestInternal<SubAccount>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), ""), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Create API Key of the sub-account
    public async Task<RestCallResult<SubAccountKey>> CreateApiKeyAsync(
        long subAccountId,
        string name = "",
        IEnumerable<SubAccountKeyPermission> permissions = null,
        IEnumerable<string> ipWhitelist = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("name", name);
        parameters.AddOptionalParameter("perms", permissions);
        parameters.AddOptionalParameter("ip_whitelist", ipWhitelist);

        return await SendRequestInternal<SubAccountKey>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), keysEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List all API Key of the sub-account
    public async Task<RestCallResult<IEnumerable<SubAccountKey>>> GetApiKeysAsync(long subAccountId, CancellationToken ct = default)
    {
        return await SendRequestInternal<IEnumerable<SubAccountKey>>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), keysEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Update API key of the sub-account
    public async Task<RestCallResult<SubAccountKey>> UpdateApiKeyAsync(
        long subAccountId,
        string apikey,
        string name = "",
        IEnumerable<SubAccountKeyPermission> permissions = null,
        IEnumerable<string> ipWhitelist = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("name", name);
        parameters.AddOptionalParameter("perms", permissions);
        parameters.AddOptionalParameter("ip_whitelist", ipWhitelist);

        return await SendRequestInternal<SubAccountKey>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Put, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Delete API key of the sub-account
    public async Task<RestCallResult<object>> DeleteApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return await SendRequestInternal<object>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Get the API Key of the sub-account
    public async Task<RestCallResult<SubAccountKey>> UpdateApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return await SendRequestInternal<SubAccountKey>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Lock the sub-account
    public async Task<RestCallResult<object>> LockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return await SendRequestInternal<object>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), lockEndpoint), HttpMethod.Put, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Unlock the sub-account
    public async Task<RestCallResult<object>> UnlockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return await SendRequestInternal<object>(RootClient.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), unlockEndpoint), HttpMethod.Put, ct, true).ConfigureAwait(false);
    }
    #endregion
}
