namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking asset type
/// </summary>
public enum GateEarnStakingAssetType : byte
{
    /// <summary>
    /// Voucher
    /// </summary>
    Voucher = 0,

    /// <summary>
    /// Locked position
    /// </summary>
    LockedPosition = 1,

    /// <summary>
    /// US Treasury bond
    /// </summary>
    UsTreasuryBond = 2,
}
