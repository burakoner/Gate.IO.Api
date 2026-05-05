namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery price-triggered order cancel request
/// </summary>
public record GateDeliveryPriceTriggeredOrderCancelRequest
{
    public string Contract { get; set; }
}
