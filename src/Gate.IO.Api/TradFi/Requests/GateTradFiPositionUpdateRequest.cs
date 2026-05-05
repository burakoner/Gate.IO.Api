namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi position update request
/// </summary>
public record GateTradFiPositionUpdateRequest
{
    public decimal? TakeProfitPrice { get; set; }
    public decimal? StopLossPrice { get; set; }
}
