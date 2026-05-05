namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order update request
/// </summary>
public record GateTradFiOrderUpdateRequest
{
    public decimal Price { get; set; }
    public decimal? TakeProfitPrice { get; set; }
    public decimal? StopLossPrice { get; set; }
}
