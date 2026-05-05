namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan repayment request
/// </summary>
public record GateMultiCollateralLoanRepayRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Repay currency items
    /// </summary>
    public IEnumerable<GateMultiCollateralLoanRepayItem> RepayItems { get; set; }
}
