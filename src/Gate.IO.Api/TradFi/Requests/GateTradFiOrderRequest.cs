namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order request
/// </summary>
public record GateTradFiOrderRequest
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    public string Symbol { get; set; }
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    public GateTradFiOrderSide Side { get; set; }
    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    public GateTradFiOrderPriceType PriceType { get; set; }
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the Volume.
    /// </summary>
    public decimal Volume { get; set; }
    /// <summary>
    /// Gets or sets the Take Profit Price.
    /// </summary>
    public decimal? TakeProfitPrice { get; set; }
    /// <summary>
    /// Gets or sets the Stop Loss Price.
    /// </summary>
    public decimal? StopLossPrice { get; set; }
}
