namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot market trade query request
/// </summary>
public record GateSpotTradeQueryRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Last record ID from previous list-query results
    /// </summary>
    public string LastId { get; set; }

    /// <summary>
    /// Whether records should be retrieved before LastId
    /// </summary>
    public bool? Reverse { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }
}
