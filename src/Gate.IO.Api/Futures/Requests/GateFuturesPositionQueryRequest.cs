namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures position-list query request
/// </summary>
public record GateFuturesPositionQueryRequest
{
    /// <summary>
    /// Gets or sets the Holding.
    /// </summary>
    public bool? Holding { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the Offset.
    /// </summary>
    public int? Offset { get; set; }
}
