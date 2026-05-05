namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery candlestick query request
/// </summary>
public record GateDeliveryCandlestickQueryRequest
{
    public string Contract { get; set; }
    public GateFuturesCandlestickInterval Interval { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
