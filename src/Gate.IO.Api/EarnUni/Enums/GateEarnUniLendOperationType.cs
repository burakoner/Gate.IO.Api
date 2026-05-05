namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending operation type
/// </summary>
public enum GateEarnUniLendOperationType : byte
{
    /// <summary>
    /// Lend
    /// </summary>
    [Map("lend")]
    Lend = 1,

    /// <summary>
    /// Redeem
    /// </summary>
    [Map("redeem")]
    Redeem = 2,
}
