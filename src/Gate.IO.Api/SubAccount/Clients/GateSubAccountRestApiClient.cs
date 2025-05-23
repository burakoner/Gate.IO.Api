namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Gate SubAccount Rest Api Client
/// </summary>
public class GateSubAccountRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string sub_accounts = "sub_accounts";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateSubAccountRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// Create a new sub-account
    /// </summary>
    /// <param name="login">Sub-account login name: Only letters, numbers and underscores are supported, and cannot contain other illegal characters</param>
    /// <param name="password">The sub-account's password. (Default: the same as main account's password)</param>
    /// <param name="email">The sub-account's email address. (Default: the same as main account's email address)</param>
    /// <param name="remark">custom text</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccount>> CreateSubAccountAsync(
        string login,
        string password = null,
        string email = null,
        string remark = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "login_name", login },
        };
        parameters.AddOptional("email", email);
        parameters.AddOptional("password", password);
        parameters.AddOptional("remark", remark);

        return _.SendRequestInternal<GateSubAccount>(_.GetUrl(api, version, sub_accounts, null), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List sub-accounts
    /// </summary>
    /// <param name="listSubAccountsOnly">type: 0 to list all types of sub-accounts (currently supporting cross margin accounts and sub-accounts). 1 to list sub-accounts only. If no parameter is passed, only sub-accounts will be listed by default.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSubAccount>>> GetSubAccountsAsync(bool listSubAccountsOnly = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "type", listSubAccountsOnly ? "1": "0" },
        };
        return _.SendRequestInternal<List<GateSubAccount>>(_.GetUrl(api, version, sub_accounts, null), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccount>> GetSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateSubAccount>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()), null), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Create API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="mode">Mode: 1 - classic 2 - portfolio account</param>
    /// <param name="name">API key name</param>
    /// <param name="permissions">Permissions</param>
    /// <param name="ipWhitelist">ip white list (list will be removed if no value is passed)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccountApiKey>> CreateApiKeyAsync(
        long subAccountId,
        int? mode = null,
        string name = null,
        IEnumerable<GateSubAccountApiKeyPermission> permissions = null,
        IEnumerable<string> ipWhitelist = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("mode", mode);
        parameters.AddOptional("name", name);
        parameters.AddOptional("perms", permissions);
        parameters.AddOptional("ip_whitelist", ipWhitelist);

        return _.SendRequestInternal<GateSubAccountApiKey>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()), "keys"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List all API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSubAccountApiKey>>> GetApiKeysAsync(long subAccountId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateSubAccountApiKey>>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()), "keys"), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Update API key of the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="apikey">The API Key of the sub-account</param>
    /// <param name="mode">Mode: 1 - classic 2 - portfolio account</param>
    /// <param name="name">API key name</param>
    /// <param name="permissions">Permissions</param>
    /// <param name="ipWhitelist">ip white list (list will be removed if no value is passed)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccountApiKey>> UpdateApiKeyAsync(
        long subAccountId,
        string apikey,
        int? mode = null,
        string name = null,
        IEnumerable<GateSubAccountApiKeyPermission> permissions = null,
        IEnumerable<string> ipWhitelist = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("mode", mode);
        parameters.AddOptional("name", name);
        parameters.AddOptional("perms", permissions);
        parameters.AddOptional("ip_whitelist", ipWhitelist);

        return _.SendRequestInternal<GateSubAccountApiKey>(
            _.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()).AppendPath("keys").AppendPath(apikey), null),
            HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Delete API key of the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="apikey">The API Key of the sub-account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> DeleteApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return _.SendRequestInternal<object>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()).AppendPath("keys").AppendPath(apikey), null), HttpMethod.Delete, ct, true);
    }

    /// <summary>
    /// Get the API Key of the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="apikey">The API Key of the sub-account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSubAccountApiKey>> GetApiKeyAsync(long subAccountId, string apikey, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateSubAccountApiKey>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()).AppendPath("keys").AppendPath(apikey), null), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Lock the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> LockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<object>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()), "lock"), HttpMethod.Put, ct, true);
    }

    /// <summary>
    /// Unlock the sub-account
    /// </summary>
    /// <param name="subAccountId">Sub-account user id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UnlockSubAccountAsync(long subAccountId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<object>(_.GetUrl(api, version, sub_accounts.AppendPath(subAccountId.ToString()), "unlock"), HttpMethod.Put, ct, true);
    }

    /// <summary>
    /// Get sub-account mode
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSubAccountMode>>> GetSubAccountsModeAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateSubAccountMode>>(_.GetUrl(api, version, sub_accounts, "unified_mode"), HttpMethod.Get, ct, true);
    }
}
