namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery order query request
/// </summary>
public record GateDeliveryOrderQueryRequest
{
    public string Contract { get; set; }
    public GateFuturesOrderStatus Status { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public long? LastId { get; set; }
    public bool? CountTotal { get; set; }
}
