namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment product query request
/// </summary>
public record GateEarnDualPlanQueryRequest
{
    /// <summary>
    /// Financial project ID
    /// </summary>
    public long? PlanId { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnDualOptionType? Type { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    public string QuoteCurrency { get; set; }

    /// <summary>
    /// Sort field
    /// </summary>
    public GateEarnDualPlanSort? Sort { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    public int? PageSize { get; set; }
}
