namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan collateral adjustment record query request
/// </summary>
public record GateMultiCollateralLoanCollateralRecordQueryRequest
{
    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Start timestamp for the query
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp for the query
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Collateral currency
    /// </summary>
    public string CollateralCurrency { get; set; }
}
