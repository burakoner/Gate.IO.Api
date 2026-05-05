namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery cancel-orders request
/// </summary>
public record GateDeliveryCancelOrdersRequest
{
    public string Contract { get; set; }
    public GateFuturesOrderSide Side { get; set; }
}
