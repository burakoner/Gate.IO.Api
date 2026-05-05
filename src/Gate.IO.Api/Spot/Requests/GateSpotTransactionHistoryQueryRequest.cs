namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot account transaction history query request
/// </summary>
public record GateSpotTransactionHistoryQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Account change type
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Account change code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }
}
