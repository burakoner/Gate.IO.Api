namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery candlestick query request
/// </summary>
public record GateDeliveryCandlestickQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Interval.
    /// </summary>
    public GateFuturesCandlestickInterval Interval { get; set; }
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
