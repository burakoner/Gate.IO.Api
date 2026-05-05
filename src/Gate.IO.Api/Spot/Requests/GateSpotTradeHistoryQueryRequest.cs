namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot personal trade history query request
/// </summary>
public record GateSpotTradeHistoryQueryRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Account type
    /// </summary>
    public GateSpotAccountType? Account { get; set; }

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

    /// <summary>
    /// Order ID
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order ID
    /// </summary>
    public string ClientOrderId { get; set; }
}
