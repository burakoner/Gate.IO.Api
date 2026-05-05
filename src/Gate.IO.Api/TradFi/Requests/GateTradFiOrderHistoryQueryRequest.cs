namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical order query request
/// </summary>
public record GateTradFiOrderHistoryQueryRequest
{
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Symbol { get; set; }
    public GateTradFiOrderSide? Side { get; set; }
}
