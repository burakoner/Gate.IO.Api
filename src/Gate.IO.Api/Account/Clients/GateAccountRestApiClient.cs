namespace Gate.IO.Api.Account;

/// <summary>
/// Gate.IO Account REST API Client
/// </summary>
public class GateAccountRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string account = "account";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateAccountRestApiClient(GateRestApiClient root) => _ = root;

    public Task<RestCallResult<GateAccountDetails>> GetAccountAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateAccountDetails>(_.GetUrl(api, v4, account, "detail"), HttpMethod.Get, ct, true);
    }

    public Task<RestCallResult<List<GateAccountRateLimit>>> GetRateLimitsAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateAccountRateLimit>>(_.GetUrl(api, v4, account, "rate_limit"), HttpMethod.Get, ct, true);
    }

    public Task<RestCallResult<GateAccountStpGroup>> CreateStpGroupAsync(string name, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("name", name);

        return _.SendRequestInternal<GateAccountStpGroup>(_.GetUrl(api, v4, account, "stp_groups"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<GateAccountStpGroup>>> GetStpGroupsAsync(string name, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("name", name);

        return _.SendRequestInternal<List<GateAccountStpGroup>>(_.GetUrl(api, v4, account, "stp_groups"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<GateAccountStpGroupUser>>> GetStpGroupUsersAsync(long stpId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateAccountStpGroupUser>>(_.GetUrl(api, v4, account, "stp_groups/{stp_id}/users".Replace("{stp_id}", stpId.ToString())), HttpMethod.Get, ct, true);
    }

    public Task<RestCallResult<List<GateAccountStpGroupUser>>> AddUserToStpGroupAsync(long stpId, IEnumerable<long> userIds, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(userIds);

        return _.SendRequestInternal<List<GateAccountStpGroupUser>>(_.GetUrl(api, v4, account, "stp_groups/{stp_id}/users".Replace("{stp_id}", stpId.ToString())), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<GateAccountStpGroupUser>>> RemoveUserToStpGroupAsync(long stpId, long userId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("user_id", userId);

        return _.SendRequestInternal<List<GateAccountStpGroupUser>>(_.GetUrl(api, v4, account, "stp_groups/{stp_id}/users".Replace("{stp_id}", stpId.ToString())), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<object>> SetGtDeductionAsync(bool enabled, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("enabled", enabled);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, account, "debit_fee"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<GateAccountGtDeduction>> GetGtDeductionAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateAccountGtDeduction>(_.GetUrl(api, v4, account, "debit_fee"), HttpMethod.Get, ct, true);
    }
}