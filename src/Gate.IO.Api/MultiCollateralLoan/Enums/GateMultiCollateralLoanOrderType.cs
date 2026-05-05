namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order type
/// </summary>
public enum GateMultiCollateralLoanOrderType : byte
{
    /// <summary>
    /// Current rate order
    /// </summary>
    [Map("current")]
    Current = 1,

    /// <summary>
    /// Fixed rate order
    /// </summary>
    [Map("fixed")]
    Fixed = 2,
}
