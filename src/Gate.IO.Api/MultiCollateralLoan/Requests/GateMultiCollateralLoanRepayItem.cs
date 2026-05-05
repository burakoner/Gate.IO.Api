namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan repayment item
/// </summary>
public record GateMultiCollateralLoanRepayItem
{
    /// <summary>
    /// Repayment currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Repayment method. True for full repayment, false for partial repayment.
    /// </summary>
    public bool RepaidAll { get; set; }
}
