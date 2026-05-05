namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking operation type
/// </summary>
public enum GateEarnStakingOperationType : byte
{
    /// <summary>
    /// Stake
    /// </summary>
    Stake = 0,

    /// <summary>
    /// Redeem
    /// </summary>
    Redeem = 1,
}
