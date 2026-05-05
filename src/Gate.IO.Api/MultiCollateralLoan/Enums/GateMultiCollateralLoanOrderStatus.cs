namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order status
/// </summary>
public enum GateMultiCollateralLoanOrderStatus : byte
{
    /// <summary>
    /// Initial state after placing the order
    /// </summary>
    [Map("initial")]
    Initial = 1,

    /// <summary>
    /// Collateral deduction successful
    /// </summary>
    [Map("collateral_deducted")]
    CollateralDeducted = 2,

    /// <summary>
    /// Loan failed and collateral return is pending
    /// </summary>
    [Map("collateral_returning")]
    CollateralReturning = 3,

    /// <summary>
    /// Loan successful
    /// </summary>
    [Map("lent")]
    Lent = 4,

    /// <summary>
    /// Repayment in progress
    /// </summary>
    [Map("repaying")]
    Repaying = 5,

    /// <summary>
    /// Liquidation in progress
    /// </summary>
    [Map("liquidating")]
    Liquidating = 6,

    /// <summary>
    /// Order completed
    /// </summary>
    [Map("finished")]
    Finished = 7,

    /// <summary>
    /// Liquidation and repayment completed
    /// </summary>
    [Map("closed_liquidated")]
    ClosedLiquidated = 8,
}
