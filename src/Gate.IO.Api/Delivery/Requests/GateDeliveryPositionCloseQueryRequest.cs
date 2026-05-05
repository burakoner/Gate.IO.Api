namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery position-close history query request
/// </summary>
public record GateDeliveryPositionCloseQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
}
