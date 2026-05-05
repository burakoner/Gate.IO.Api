namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan fixed period
/// </summary>
public enum GateMultiCollateralLoanFixedType : byte
{
    /// <summary>
    /// Seven days
    /// </summary>
    [Map("7d")]
    SevenDays = 1,

    /// <summary>
    /// Thirty days
    /// </summary>
    [Map("30d")]
    ThirtyDays = 2,
}
