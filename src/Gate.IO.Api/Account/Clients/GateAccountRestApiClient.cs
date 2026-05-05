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

    private static string StpGroupUsersPath(long stpId)
        => "stp_groups".AppendPath(stpId.ToString()).AppendPath("users");

    /// <summary>
    /// Retrieve user account information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAccountDetails>> GetAccountAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateAccountDetails>(_.GetUrl(api, v4, account, "detail"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Retrieve user account information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAccountDetails>> GetAccountDetailsAsync(CancellationToken ct = default)
        => GetAccountAsync(ct);

    /// <summary>
    /// Query all main account key information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountKeyInfo>>> GetMainKeysAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateAccountKeyInfo>>(_.GetUrl(api, v4, account, "main_keys"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Get user transaction rate limit information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountRateLimit>>> GetRateLimitsAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateAccountRateLimit>>(_.GetUrl(api, v4, account, "rate_limit"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Create STP user group
    /// </summary>
    /// <param name="name">STP group name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAccountStpGroup>> CreateStpGroupAsync(string name, CancellationToken ct = default)
        => CreateStpGroupAsync(new GateAccountStpGroupRequest { Name = name }, ct);

    /// <summary>
    /// Create STP user group
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAccountStpGroup>> CreateStpGroupAsync(GateAccountStpGroupRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "name", request.Name },
        };

        return _.SendRequestInternal<GateAccountStpGroup>(_.GetUrl(api, v4, account, "stp_groups"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query STP user groups created by the user
    /// </summary>
    /// <param name="name">Fuzzy search by name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroup>>> GetStpGroupsAsync(string name = null, CancellationToken ct = default)
        => GetStpGroupsAsync(new GateAccountStpGroupQueryRequest { Name = name }, ct);

    /// <summary>
    /// Query STP user groups created by the user
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroup>>> GetStpGroupsAsync(GateAccountStpGroupQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("name", request.Name);

        return _.SendRequestInternal<List<GateAccountStpGroup>>(_.GetUrl(api, v4, account, "stp_groups"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query users in the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> GetStpGroupUsersAsync(long stpId, CancellationToken ct = default)
        => _.SendRequestInternal<List<GateAccountStpGroupUser>>(_.GetUrl(api, v4, account, StpGroupUsersPath(stpId)), HttpMethod.Get, ct, true);

    /// <summary>
    /// Add users to the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="userIds">User IDs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> AddUserToStpGroupAsync(long stpId, IEnumerable<long> userIds, CancellationToken ct = default)
        => AddUsersToStpGroupAsync(stpId, new GateAccountStpGroupUsersRequest { UserIds = userIds }, ct);

    /// <summary>
    /// Add users to the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="userIds">User IDs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> AddUsersToStpGroupAsync(long stpId, IEnumerable<long> userIds, CancellationToken ct = default)
        => AddUsersToStpGroupAsync(stpId, new GateAccountStpGroupUsersRequest { UserIds = userIds }, ct);

    /// <summary>
    /// Add users to the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> AddUsersToStpGroupAsync(long stpId, GateAccountStpGroupUsersRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(request.UserIds ?? Enumerable.Empty<long>());

        return _.SendRequestInternal<List<GateAccountStpGroupUser>>(_.GetUrl(api, v4, account, StpGroupUsersPath(stpId)), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Delete a user from the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> RemoveUserToStpGroupAsync(long stpId, long userId, CancellationToken ct = default)
        => RemoveUsersFromStpGroupAsync(stpId, new GateAccountStpGroupUsersRequest { UserIds = new[] { userId } }, ct);

    /// <summary>
    /// Delete a user from the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> RemoveUserFromStpGroupAsync(long stpId, long userId, CancellationToken ct = default)
        => RemoveUsersFromStpGroupAsync(stpId, new GateAccountStpGroupUsersRequest { UserIds = new[] { userId } }, ct);

    /// <summary>
    /// Delete users from the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="userIds">User IDs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> RemoveUsersFromStpGroupAsync(long stpId, IEnumerable<long> userIds, CancellationToken ct = default)
        => RemoveUsersFromStpGroupAsync(stpId, new GateAccountStpGroupUsersRequest { UserIds = userIds }, ct);

    /// <summary>
    /// Delete users from the STP user group
    /// </summary>
    /// <param name="stpId">STP Group ID</param>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAccountStpGroupUser>>> RemoveUsersFromStpGroupAsync(long stpId, GateAccountStpGroupUsersRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("user_id", string.Join(",", request.UserIds ?? Enumerable.Empty<long>()));

        return _.SendRequestInternal<List<GateAccountStpGroupUser>>(_.GetUrl(api, v4, account, StpGroupUsersPath(stpId)), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Configure GT fee deduction
    /// </summary>
    /// <param name="enabled">Whether GT fee deduction is enabled</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetGtDeductionAsync(bool enabled, CancellationToken ct = default)
        => SetGtDeductionAsync(new GateAccountDebitFeeRequest { Enabled = enabled }, ct);

    /// <summary>
    /// Configure GT fee deduction
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetGtDeductionAsync(GateAccountDebitFeeRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("enabled", request.Enabled);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, account, "debit_fee"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Configure GT fee deduction
    /// </summary>
    /// <param name="enabled">Whether GT fee deduction is enabled</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetDebitFeeAsync(bool enabled, CancellationToken ct = default)
        => SetGtDeductionAsync(enabled, ct);

    /// <summary>
    /// Configure GT fee deduction
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetDebitFeeAsync(GateAccountDebitFeeRequest request, CancellationToken ct = default)
        => SetGtDeductionAsync(request, ct);

    /// <summary>
    /// Query GT fee deduction configuration
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAccountGtDeduction>> GetGtDeductionAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateAccountGtDeduction>(_.GetUrl(api, v4, account, "debit_fee"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query GT fee deduction configuration
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAccountGtDeduction>> GetDebitFeeAsync(CancellationToken ct = default)
        => GetGtDeductionAsync(ct);
}
