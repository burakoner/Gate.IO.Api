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
    public Task<RestCallResult<GateSubAccount>> CreateSubAccountAsync(
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

        return Root.SendRequestInternal<GateSubAccount>(Root.GetUrl(api, version, subaccounts, ""), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List sub-accounts
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSubAccount>>> GetSubAccountsAsync(CancellationToken ct = default)
    {
        return Root.SendRequestInternal<List<GateSubAccount>>(Root.GetUrl(api, version, subaccounts, ""), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Get the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccount>> GetSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return Root.SendRequestInternal<GateSubAccount>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), ""), HttpMethod.Get, ct, true);
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
    public Task<RestCallResult<GateSubAccountApiKey>> CreateApiKeyAsync(
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

        return Root.SendRequestInternal<GateSubAccountApiKey>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), keysEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List all API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSubAccountApiKey>>> GetApiKeysAsync(long subAccountId, CancellationToken ct = default)
    {
        return Root.SendRequestInternal<List<GateSubAccountApiKey>>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), keysEndpoint), HttpMethod.Get, ct, true);
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
    public Task<RestCallResult<GateSubAccountApiKey>> UpdateApiKeyAsync(
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

        return Root.SendRequestInternal<GateSubAccountApiKey>(
            Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""),
            HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Delete API key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="apikey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<object>> DeleteApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return Root.SendRequestInternal<object>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Delete, ct, true);
    }

    /// <summary>
    /// Get the API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="apikey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccountApiKey>> GetApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return Root.SendRequestInternal<GateSubAccountApiKey>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()).AppendPath(keysEndpoint).AppendPath(apikey), ""), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Lock the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<object>> LockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return Root.SendRequestInternal<object>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), lockEndpoint), HttpMethod.Put, ct, true);
    }

    /// <summary>
    /// Unlock the sub-account
    /// </summary>
    /// <param name="subAccountId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UnlockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return Root.SendRequestInternal<object>(Root.GetUrl(api, version, subaccounts.AppendPath(subAccountId.ToString()), unlockEndpoint), HttpMethod.Put, ct, true);
    }

    // TODO: Get sub-account mode
}
