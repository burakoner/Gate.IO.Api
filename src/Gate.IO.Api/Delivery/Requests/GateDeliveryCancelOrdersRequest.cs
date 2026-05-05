namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery cancel-orders request
/// </summary>
public record GateDeliveryCancelOrdersRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    public GateFuturesOrderSide Side { get; set; }
}
