namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi kline query request
/// </summary>
public record GateTradFiCandlestickQueryRequest
{
    public string Symbol { get; set; }
    public GateTradFiKlineInterval Interval { get; set; }
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? Limit { get; set; }
}
