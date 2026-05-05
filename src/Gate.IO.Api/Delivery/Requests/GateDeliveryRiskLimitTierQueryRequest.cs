namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery risk-limit tier query request
/// </summary>
public record GateDeliveryRiskLimitTierQueryRequest
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
    /// Gets or sets the Offset.
    /// </summary>
    public long? Offset { get; set; }
}
