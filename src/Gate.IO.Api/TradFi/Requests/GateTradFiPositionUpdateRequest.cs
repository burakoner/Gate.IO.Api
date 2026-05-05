namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi position update request
/// </summary>
public record GateTradFiPositionUpdateRequest
{
    /// <summary>
    /// Gets or sets the Take Profit Price.
    /// </summary>
    public decimal? TakeProfitPrice { get; set; }
    /// <summary>
    /// Gets or sets the Stop Loss Price.
    /// </summary>
    public decimal? StopLossPrice { get; set; }
}
