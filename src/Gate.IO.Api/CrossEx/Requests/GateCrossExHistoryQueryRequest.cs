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
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Start time, serialized as a Unix timestamp in milliseconds
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time, serialized as a Unix timestamp in milliseconds
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Order attributes used to filter historical orders. This property is ignored by other history endpoints.
    /// </summary>
    public IEnumerable<GateCrossExOrderAttribute> Attributes { get; set; }
}
