namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order history query
/// </summary>
public record GateStockOrderHistoryQueryRequest
{
    /// <summary>Gets or sets the symbol.</summary>
    public string Symbol { get; set; }
    /// <summary>Gets or sets up to 20 positive order identifiers.</summary>
    public IEnumerable<long> OrderIds { get; set; }
    /// <summary>Gets or sets the beginning time.</summary>
    public DateTime? BeginTime { get; set; }
    /// <summary>Gets or sets the ending time.</summary>
    public DateTime? EndTime { get; set; }
    /// <summary>Gets or sets the order side.</summary>
    public GateStockOrderSide? Side { get; set; }
    /// <summary>Gets or sets the page number.</summary>
    public int? Page { get; set; }
    /// <summary>Gets or sets the page size, up to 500.</summary>
    public int? PageSize { get; set; }
}
