namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery price-triggered order query request
/// </summary>
public record GateDeliveryPriceTriggeredOrderQueryRequest
{
    public GateSpotTriggerFilter Status { get; set; }
    public string Contract { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
