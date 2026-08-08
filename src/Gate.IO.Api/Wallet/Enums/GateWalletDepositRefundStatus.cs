namespace Gate.IO.Api.Wallet;

/// <summary>
/// Blocked deposit refund status
/// </summary>
public enum GateWalletDepositRefundStatus : byte
{
    /// <summary>
    /// Refund in progress
    /// </summary>
    [Map("REFUNDING")]
    Refunding = 1,

    /// <summary>
    /// Refund completed
    /// </summary>
    [Map("REFUNDED")]
    Refunded = 2,

    /// <summary>
    /// Refund failed
    /// </summary>
    [Map("REFUND_FAILED")]
    Failed = 3,

    /// <summary>
    /// Refund rejected
    /// </summary>
    [Map("REJECTED")]
    Rejected = 4,
}
