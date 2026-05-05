namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery public trade query request
/// </summary>
public record GateDeliveryTradeQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public long? LastId { get; set; }
}
