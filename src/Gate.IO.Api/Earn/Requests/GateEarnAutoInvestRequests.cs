namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest portfolio item
/// </summary>
public record GateEarnAutoInvestPortfolioItem
{
    /// <summary>
    /// Investment currency
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Portfolio ratio
    /// </summary>
    public decimal Ratio { get; set; }
}

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

/// <summary>
/// Auto invest add position request
/// </summary>
public record GateEarnAutoInvestAddPositionRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public decimal Amount { get; set; }
}

/// <summary>
/// Auto invest minimum amount request
/// </summary>
public record GateEarnAutoInvestMinInvestAmountRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Money { get; set; }

    /// <summary>
    /// Investment portfolio
    /// </summary>
    public IEnumerable<GateEarnAutoInvestPortfolioItem> Items { get; set; }
}

/// <summary>
/// Auto invest plan execution records request
/// </summary>
public record GateEarnAutoInvestExecutionRecordsRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public long? Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    public long? PageSize { get; set; }
}

/// <summary>
/// Auto invest order details request
/// </summary>
public record GateEarnAutoInvestOrderDetailsRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Record ID
    /// </summary>
    public long RecordId { get; set; }
}

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
