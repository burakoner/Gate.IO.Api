namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order update request
/// </summary>
public record GateFuturesTrailOrderUpdateRequest
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    public long OrderId { get; set; }
    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    public decimal? Amount { get; set; }
    /// <summary>
    /// Gets or sets the Activation Price.
    /// </summary>
    public decimal? ActivationPrice { get; set; }
    /// <summary>
    /// Gets or sets the Is Greater Than Or Equal.
    /// </summary>
    public bool? IsGreaterThanOrEqual { get; set; }
    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    public GateFuturesTrailPriceType? PriceType { get; set; }
    /// <summary>
    /// Gets or sets the Price Offset.
    /// </summary>
    public string PriceOffset { get; set; }
}
