namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery settlement-history query request
/// </summary>
public record GateDeliverySettlementQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the At.
    /// </summary>
    public DateTime? At { get; set; }
}
