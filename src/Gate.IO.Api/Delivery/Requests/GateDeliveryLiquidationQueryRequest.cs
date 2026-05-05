namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery user liquidation-history query request
/// </summary>
public record GateDeliveryLiquidationQueryRequest
{
    public string Contract { get; set; }
    public int? Limit { get; set; }
    public DateTime? At { get; set; }
}
