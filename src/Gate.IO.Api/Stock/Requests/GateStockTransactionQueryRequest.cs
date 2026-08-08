namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock transaction query
/// </summary>
public record GateStockTransactionQueryRequest
{
    /// <summary>Gets or sets the beginning time.</summary>
    public DateTime? BeginTime { get; set; }
    /// <summary>Gets or sets the ending time.</summary>
    public DateTime? EndTime { get; set; }
    /// <summary>Gets or sets the reference identifier. Other filters are ignored by Gate when supplied.</summary>
    public string ReferenceId { get; set; }
    /// <summary>Gets or sets the transaction type.</summary>
    public GateStockTransactionType? Type { get; set; }
    /// <summary>Gets or sets the page number.</summary>
    public int? Page { get; set; }
    /// <summary>Gets or sets the page size, up to 500.</summary>
    public int? PageSize { get; set; }
}
