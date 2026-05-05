namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified loans query request
/// </summary>
public record GateUnifiedLoanQueryRequest
{
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

    /// <summary>
    /// Loan type
    /// </summary>
    public GateUnifiedLoanType? Type { get; set; }
}
