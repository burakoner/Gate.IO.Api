namespace Gate.IO.Api.Options;

/// <summary>
/// Options candlestick query request
/// </summary>
public record GateOptionsCandlestickQueryRequest
{
    public string Contract { get; set; }
    public GateOptionsCandlestickInterval? Interval { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
