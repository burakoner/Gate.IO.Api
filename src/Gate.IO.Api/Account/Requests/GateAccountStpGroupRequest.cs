namespace Gate.IO.Api.Account;

/// <summary>
/// Account STP group request
/// </summary>
public record GateAccountStpGroupRequest
{
    /// <summary>
    /// STP group name
    /// </summary>
    public string Name { get; set; }
}
