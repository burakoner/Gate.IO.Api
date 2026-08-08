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
    /// Maximum number of positions to return, from 1 to 100. Omit to return all current positions.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// List offset, starting from 0. Omit to use the API default.
    /// </summary>
    public int? Offset { get; set; }
}
