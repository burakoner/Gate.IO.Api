namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking currency type
/// </summary>
public enum GateEarnStakingCoinType : byte
{
    /// <summary>
    /// Voucher
    /// </summary>
    [Map("swap")]
    Swap = 1,

    /// <summary>
    /// Locked position
    /// </summary>
    [Map("lock")]
    Lock = 2,

    /// <summary>
    /// US Treasury bond
    /// </summary>
    [Map("debt")]
    Debt = 3,
}
