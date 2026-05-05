namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn history type
/// </summary>
public enum GateEarnFixedTermHistoryType : byte
{
    /// <summary>
    /// Subscription
    /// </summary>
    Subscription = 1,

    /// <summary>
    /// Redemption
    /// </summary>
    Redemption = 2,

    /// <summary>
    /// Interest
    /// </summary>
    Interest = 3,

    /// <summary>
    /// Bonus reward
    /// </summary>
    BonusReward = 4,
}
