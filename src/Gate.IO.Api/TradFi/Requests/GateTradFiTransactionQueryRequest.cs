namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction query request
/// </summary>
public record GateTradFiTransactionQueryRequest
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
    /// Gets or sets the Type.
    /// </summary>
    public GateTradFiTransactionType? Type { get; set; }
    /// <summary>
    /// Gets or sets the Page.
    /// </summary>
    public int? Page { get; set; }
    /// <summary>
    /// Gets or sets the Page Size.
    /// </summary>
    public int? PageSize { get; set; }
}
