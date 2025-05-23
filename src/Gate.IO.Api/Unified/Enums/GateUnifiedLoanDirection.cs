namespace Gate.IO.Api.Unified;

/// <summary>
/// Borrow direction
/// </summary>
public enum GateUnifiedLoanDirection : byte
{
    /// <summary>
    /// Borrow
    /// </summary>
    [Map("borrow")]
    Borrow = 1,

    /// <summary>
    /// Repay
    /// </summary>
    [Map("repay")]
    Repay = 2
}
