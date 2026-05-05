namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery settlement-history query request
/// </summary>
public record GateDeliverySettlementQueryRequest
{
    public string Contract { get; set; }
    public int? Limit { get; set; }
    public DateTime? At { get; set; }
}
