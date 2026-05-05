namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order change-log query request
/// </summary>
public record GateFuturesTrailOrderChangeLogQueryRequest
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    public long OrderId { get; set; }
    /// <summary>
    /// Gets or sets the Page Number.
    /// </summary>
    public int? PageNumber { get; set; }
    /// <summary>
    /// Gets or sets the Page Size.
    /// </summary>
    public int? PageSize { get; set; }
}
