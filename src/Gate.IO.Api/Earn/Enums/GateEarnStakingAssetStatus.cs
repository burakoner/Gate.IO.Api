namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking asset project status
/// </summary>
public enum GateEarnStakingAssetStatus : byte
{
    /// <summary>
    /// Currency project is listed
    /// </summary>
    Listed = 1,

    /// <summary>
    /// Currency project is delisted
    /// </summary>
    Delisted = 2,
}
