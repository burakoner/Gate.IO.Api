namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order sort type
/// </summary>
public enum GateMultiCollateralLoanOrderSort : byte
{
    /// <summary>
    /// Descending by creation time
    /// </summary>
    [Map("time_desc")]
    TimeDescending = 1,

    /// <summary>
    /// Ascending by LTV ratio
    /// </summary>
    [Map("ltv_asc")]
    LtvAscending = 2,

    /// <summary>
    /// Descending by LTV ratio
    /// </summary>
    [Map("ltv_desc")]
    LtvDescending = 3,
}
