namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan collateral operation type
/// </summary>
public enum GateMultiCollateralLoanCollateralOperationType : byte
{
    /// <summary>
    /// Add collateral
    /// </summary>
    [Map("append")]
    Append = 1,

    /// <summary>
    /// Withdraw collateral
    /// </summary>
    [Map("redeem")]
    Redeem = 2,
}
