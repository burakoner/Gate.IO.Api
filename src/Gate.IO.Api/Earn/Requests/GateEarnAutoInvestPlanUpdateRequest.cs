namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan update request
/// </summary>
public record GateEarnAutoInvestPlanUpdateRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Fund source
    /// </summary>
    public GateEarnAutoInvestFundSource? FundSource { get; set; }

    /// <summary>
    /// Fund flow direction
    /// </summary>
    public GateEarnAutoInvestFundFlow? FundFlow { get; set; }
}
