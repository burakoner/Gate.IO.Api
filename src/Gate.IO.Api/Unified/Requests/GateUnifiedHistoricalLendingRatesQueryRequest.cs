namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified historical lending rates query request
/// </summary>
public record GateUnifiedHistoricalLendingRatesQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// VIP level for the floating rate to be queried
    /// </summary>
    public string Tier { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of items returned
    /// </summary>
    public int? Limit { get; set; }
}
