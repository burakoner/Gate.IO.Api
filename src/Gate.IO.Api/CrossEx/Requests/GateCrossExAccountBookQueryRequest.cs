namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account book query request
/// </summary>
public record GateCrossExAccountBookQueryRequest
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
    /// Currency
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Statement type
    /// </summary>
    public string StatementType { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? To { get; set; }
}
