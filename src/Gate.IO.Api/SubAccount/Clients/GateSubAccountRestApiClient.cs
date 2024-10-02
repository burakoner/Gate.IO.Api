namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Gate SubAccount Rest Api Client
/// </summary>
public class GateSubAccountRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string subaccounts = "sub_accounts";

    // Endpoints
    private const string keysEndpoint = "keys";
    private const string lockEndpoint = "lock";
    private const string unlockEndpoint = "unlock";

    // Root Client
    internal GateRestApiClient Root { get; }

    internal GateSubAccountRestApiClient(GateRestApiClient root)
    {
        Root = root;
    }

    /// <summary>
    /// Create a new sub-account
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <param name="remark"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSubAccount>> CreateSubAccountAsync(
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

        return await Root.SendRequestInternal<GateSubAccount>(Root.GetUrl(api, version, subaccounts, ""), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List sub-accounts
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSubAccount>>> GetSubAccountsAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<List<GateSubAccount>>(Root.GetUrl(api, version, subaccounts, ""), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSubAccount>> GetSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<GateSubAccount>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), ""), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Create API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="mode"></param>
    /// <param name="name"></param>
    /// <param name="permissions"></param>
    /// <param name="ipWhitelist"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSubAccountApiKey>> CreateApiKeyAsync(
        long subAccountId,
        int? mode = null,
        string name = "",
        IEnumerable<GateSubAccountApiKeyPermission> permissions = null,
        IEnumerable<string> ipWhitelist = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("mode", mode);
        parameters.AddOptionalParameter("name", name);
        parameters.AddOptionalParameter("perms", permissions);
        parameters.AddOptionalParameter("ip_whitelist", ipWhitelist);

        return await Root.SendRequestInternal<GateSubAccountApiKey>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), keysEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// List all API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSubAccountApiKey>>> GetApiKeysAsync(long subAccountId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<List<GateSubAccountApiKey>>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), keysEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Update API key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="apikey"></param>
    /// <param name="mode"></param>
    /// <param name="name"></param>
    /// <param name="permissions"></param>
    /// <param name="ipWhitelist"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSubAccountApiKey>> UpdateApiKeyAsync(
        long subAccountId,
        string apikey,
        int? mode = null,
        string name = "",
        IEnumerable<GateSubAccountApiKeyPermission> permissions = null,
        IEnumerable<string> ipWhitelist = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("mode", mode);
        parameters.AddOptionalParameter("name", name);
        parameters.AddOptionalParameter("perms", permissions);
        parameters.AddOptionalParameter("ip_whitelist", ipWhitelist);

        return await Root.SendRequestInternal<GateSubAccountApiKey>(
            Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), 
            HttpMethod.Put, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete API key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="apikey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<object>> DeleteApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<object>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="apikey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSubAccountApiKey>> GetApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<GateSubAccountApiKey>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Lock the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<object>> LockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<object>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), lockEndpoint), HttpMethod.Put, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Unlock the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<object>> UnlockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<object>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), unlockEndpoint), HttpMethod.Put, ct, true).ConfigureAwait(false);
    }

    // TODO: Get sub-account mode
}
