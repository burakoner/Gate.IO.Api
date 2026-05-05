namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical position query request
/// </summary>
public record GateTradFiPositionHistoryQueryRequest
{
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Symbol { get; set; }
    public GateTradFiPositionDirection? Direction { get; set; }
}
