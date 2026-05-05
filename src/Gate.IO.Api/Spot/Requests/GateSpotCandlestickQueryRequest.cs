namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot candlestick query request
/// </summary>
public record GateSpotCandlestickQueryRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Candlestick interval
    /// </summary>
    public GateSpotCandlestickInterval Interval { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum recent data points to return
    /// </summary>
    public int? Limit { get; set; }
}
