namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery risk-limit tier query request
/// </summary>
public record GateDeliveryRiskLimitTierQueryRequest
{
    public string Contract { get; set; }
    public int? Limit { get; set; }
    public long? Offset { get; set; }
}
