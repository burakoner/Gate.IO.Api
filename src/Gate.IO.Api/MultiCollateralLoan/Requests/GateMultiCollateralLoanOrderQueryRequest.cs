namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order query request
/// </summary>
public record GateMultiCollateralLoanOrderQueryRequest
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
    /// Sort type
    /// </summary>
    public GateMultiCollateralLoanOrderSort? Sort { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    public GateMultiCollateralLoanOrderType? OrderType { get; set; }
}
