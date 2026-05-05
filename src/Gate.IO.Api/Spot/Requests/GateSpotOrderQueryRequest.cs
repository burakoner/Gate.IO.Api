namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot orders query request
/// </summary>
public record GateSpotOrderQueryRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public GateSpotOrderQueryStatus Status { get; set; }

    /// <summary>
    /// Account type
    /// </summary>
    public GateSpotAccountType? Account { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    public GateSpotOrderSide? Side { get; set; }

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
