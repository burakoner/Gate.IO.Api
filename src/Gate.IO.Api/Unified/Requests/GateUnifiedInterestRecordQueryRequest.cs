namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified interest deduction records query request
/// </summary>
public record GateUnifiedInterestRecordQueryRequest
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

    /// <summary>
    /// Start timestamp for the query
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp for the query
    /// </summary>
    public DateTime? To { get; set; }
}
