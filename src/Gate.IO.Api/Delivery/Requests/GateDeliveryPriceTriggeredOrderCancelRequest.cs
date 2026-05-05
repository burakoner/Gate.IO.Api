namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery price-triggered order cancel request
/// </summary>
public record GateDeliveryPriceTriggeredOrderCancelRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
}
