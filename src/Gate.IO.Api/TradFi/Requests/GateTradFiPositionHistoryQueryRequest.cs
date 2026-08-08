namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical position query request
/// </summary>
public record GateTradFiPositionHistoryQueryRequest
{
    /// <summary>
    /// Page number, starting from one.
    /// </summary>
    public int? Page { get; set; }
    /// <summary>
    /// Number of records per page. The maximum is 100.
    /// </summary>
    public int? PageSize { get; set; }
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
    /// Gets or sets the Direction.
    /// </summary>
    public GateTradFiPositionDirection? Direction { get; set; }
}
