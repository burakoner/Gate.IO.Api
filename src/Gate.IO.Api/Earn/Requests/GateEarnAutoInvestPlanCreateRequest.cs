namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan create request
/// </summary>
public record GateEarnAutoInvestPlanCreateRequest
{
    /// <summary>
    /// Plan name
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Plan description
    /// </summary>
    public string PlanDescription { get; set; }

    /// <summary>
    /// Pricing currency
    /// </summary>
    public string PlanMoney { get; set; }

    /// <summary>
    /// Per-period auto invest amount
    /// </summary>
    public decimal PlanAmount { get; set; }

    /// <summary>
    /// Cycle type
    /// </summary>
    public GateEarnAutoInvestPeriodType PeriodType { get; set; }

    /// <summary>
    /// Cycle day
    /// </summary>
    public long PeriodDay { get; set; }

    /// <summary>
    /// Execution hour
    /// </summary>
    public long PeriodHour { get; set; }

    /// <summary>
    /// Investment portfolio
    /// </summary>
    public IEnumerable<GateEarnAutoInvestPortfolioItem> Items { get; set; }

    /// <summary>
    /// Fund source
    /// </summary>
    public GateEarnAutoInvestFundSource? FundSource { get; set; }

    /// <summary>
    /// Fund flow direction
    /// </summary>
    public GateEarnAutoInvestFundFlow? FundFlow { get; set; }

    /// <summary>
    /// Creation type
    /// </summary>
    public GateEarnAutoInvestCreationType? Type { get; set; }
}
