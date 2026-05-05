namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery personal trade query request
/// </summary>
public record GateDeliveryUserTradeQueryRequest
{
    public string Contract { get; set; }
    public long? OrderId { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public long? LastId { get; set; }
    public bool? CountTotal { get; set; }
}
