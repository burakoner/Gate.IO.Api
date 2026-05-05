namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical order query request
/// </summary>
public record GateTradFiOrderHistoryQueryRequest
{
    /// <summary>
    /// Gets or sets the Begin Time.
    /// </summary>
    public DateTime? BeginTime { get; set; }
    /// <summary>
    /// Gets or sets the End Time.
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    public string Symbol { get; set; }
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    public GateTradFiOrderSide? Side { get; set; }
}
