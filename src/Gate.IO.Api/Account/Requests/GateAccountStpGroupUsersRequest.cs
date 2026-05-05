namespace Gate.IO.Api.Account;

/// <summary>
/// Account STP group users request
/// </summary>
public record GateAccountStpGroupUsersRequest
{
    /// <summary>
    /// User IDs
    /// </summary>
    public IEnumerable<long> UserIds { get; set; }
}
