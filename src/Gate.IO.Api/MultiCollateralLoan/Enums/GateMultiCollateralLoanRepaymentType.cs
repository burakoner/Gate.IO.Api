namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan repayment record type
/// </summary>
public enum GateMultiCollateralLoanRepaymentType : byte
{
    /// <summary>
    /// Regular repayment
    /// </summary>
    [Map("repay")]
    Repay = 1,

    /// <summary>
    /// Liquidation
    /// </summary>
    [Map("liquidate")]
    Liquidate = 2,
}
