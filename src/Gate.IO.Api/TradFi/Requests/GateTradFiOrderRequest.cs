namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order request
/// </summary>
public record GateTradFiOrderRequest
{
    public string Symbol { get; set; }
    public GateTradFiOrderSide Side { get; set; }
    public GateTradFiOrderPriceType PriceType { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
    public decimal? TakeProfitPrice { get; set; }
    public decimal? StopLossPrice { get; set; }
}
