namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery price-triggered order query request
/// </summary>
public record GateDeliveryPriceTriggeredOrderQueryRequest
{
    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    public GateSpotTriggerFilter Status { get; set; }
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the Offset.
    /// </summary>
    public int? Offset { get; set; }
}
