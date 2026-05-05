namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order cancel request
/// </summary>
public record GateFuturesTrailOrderCancelRequest
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    public long? OrderId { get; set; }
    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    public string ClientOrderId { get; set; }
}
