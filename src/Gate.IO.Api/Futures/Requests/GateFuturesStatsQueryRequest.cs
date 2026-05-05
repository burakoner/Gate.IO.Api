namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures statistics query request
/// </summary>
public record GateFuturesStatsQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Interval.
    /// </summary>
    public GateFuturesStatsInterval? Interval { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
}
