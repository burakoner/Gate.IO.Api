namespace Gate.IO.Api.Account;

/// <summary>
/// Account STP group query request
/// </summary>
public record GateAccountStpGroupQueryRequest
{
    /// <summary>
    /// Fuzzy search by name
    /// </summary>
    public string Name { get; set; }
}
