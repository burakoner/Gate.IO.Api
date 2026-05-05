namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order update request
/// </summary>
public record GateTradFiOrderUpdateRequest
{
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the Take Profit Price.
    /// </summary>
    public decimal? TakeProfitPrice { get; set; }
    /// <summary>
    /// Gets or sets the Stop Loss Price.
    /// </summary>
    public decimal? StopLossPrice { get; set; }
}
