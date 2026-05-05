namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan list request
/// </summary>
public record GateEarnAutoInvestPlanListRequest
{
    /// <summary>
    /// Plan status
    /// </summary>
    public GateEarnAutoInvestPlanStatus Status { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public long? Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    public long? PageSize { get; set; }
}
