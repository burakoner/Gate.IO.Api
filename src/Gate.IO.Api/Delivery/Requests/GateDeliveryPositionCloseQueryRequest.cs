namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery position-close history query request
/// </summary>
public record GateDeliveryPositionCloseQueryRequest
{
    public string Contract { get; set; }
    public int? Limit { get; set; }
}
