namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified loan records query request
/// </summary>
public record GateUnifiedLoanRecordQueryRequest
{
    /// <summary>
    /// Loan record type
    /// </summary>
    public GateUnifiedLoanDirection? Type { get; set; }

    /// <summary>
    /// Query by specified currency name
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of items returned
    /// </summary>
    public int? Limit { get; set; }
}
