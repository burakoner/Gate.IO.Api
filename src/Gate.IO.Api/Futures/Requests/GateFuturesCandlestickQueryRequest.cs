namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures candlestick query request
/// </summary>
public record GateFuturesCandlestickQueryRequest
{
    public string Contract { get; set; }
    public GateFuturesCandlestickInterval Interval { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
