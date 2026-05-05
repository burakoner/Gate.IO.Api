namespace Gate.IO.Api.Options;

/// <summary>
/// Options underlying candlestick query request
/// </summary>
public record GateOptionsUnderlyingCandlestickQueryRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
    /// <summary>
    /// Gets or sets the Interval.
    /// </summary>
    public GateOptionsCandlestickInterval? Interval { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the To.
    /// </summary>
    public DateTime? To { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
}
