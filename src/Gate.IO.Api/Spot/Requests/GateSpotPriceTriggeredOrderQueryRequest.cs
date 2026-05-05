namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot price-triggered orders query request
/// </summary>
public record GateSpotPriceTriggeredOrderQueryRequest
{
    /// <summary>
    /// Trigger order status filter
    /// </summary>
    public GateSpotTriggerFilter Status { get; set; }

    /// <summary>
    /// Trading account type
    /// </summary>
    public GateSpotPriceTriggeredOrderAccountType? Account { get; set; }

    /// <summary>
    /// Trading market
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// List offset
    /// </summary>
    public int? Offset { get; set; }
}
