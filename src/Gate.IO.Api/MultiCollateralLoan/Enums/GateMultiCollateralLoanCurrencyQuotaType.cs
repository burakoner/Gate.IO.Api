namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan quota currency type
/// </summary>
public enum GateMultiCollateralLoanCurrencyQuotaType : byte
{
    /// <summary>
    /// Collateral currency
    /// </summary>
    [Map("collateral")]
    Collateral = 1,

    /// <summary>
    /// Borrowing currency
    /// </summary>
    [Map("borrow")]
    Borrow = 2,
}
