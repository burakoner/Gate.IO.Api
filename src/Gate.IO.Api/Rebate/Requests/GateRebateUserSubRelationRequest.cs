namespace Gate.IO.Api.Rebate;

/// <summary>
/// User subordinate relationship request
/// </summary>
public record GateRebateUserSubRelationRequest
{
    /// <summary>
    /// Query user ID list. If more than 100, only 100 will be sent
    /// </summary>
    public IEnumerable<long> UserIds { get; set; }
}
