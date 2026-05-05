namespace Gate.IO.Api.Options;

/// <summary>
/// Options underlying candlestick query request
/// </summary>
public record GateOptionsUnderlyingCandlestickQueryRequest
{
    public string Underlying { get; set; }
    public GateOptionsCandlestickInterval? Interval { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
