namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi kline query request
/// </summary>
public record GateTradFiCandlestickQueryRequest
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    public string Symbol { get; set; }
    /// <summary>
    /// Gets or sets the Interval.
    /// </summary>
    public GateTradFiKlineInterval Interval { get; set; }
    /// <summary>
    /// Gets or sets the Begin Time.
    /// </summary>
    public DateTime? BeginTime { get; set; }
    /// <summary>
    /// Gets or sets the End Time.
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
}
