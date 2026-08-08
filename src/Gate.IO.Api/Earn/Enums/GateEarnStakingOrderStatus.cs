namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking order status
/// </summary>
public enum GateEarnStakingOrderStatus : byte
{
    /// <summary>
    /// Order completed successfully
    /// </summary>
    Success = 1,

    /// <summary>
    /// Delayed redemption is in progress
    /// </summary>
    DelayedRedemptionInProgress = 3,

    /// <summary>
    /// Redemption cancellation order
    /// </summary>
    RedemptionCancellation = 6,
}
