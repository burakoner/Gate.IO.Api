namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan stop request
/// </summary>
public record GateEarnAutoInvestPlanStopRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }
}
