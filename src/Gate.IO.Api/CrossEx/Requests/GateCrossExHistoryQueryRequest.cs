namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx history query request
/// </summary>
public record GateCrossExHistoryQueryRequest
{
    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum records, max 1000
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? To { get; set; }
}
