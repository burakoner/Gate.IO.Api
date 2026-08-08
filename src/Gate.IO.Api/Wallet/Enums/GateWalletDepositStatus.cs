namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet deposit status
/// </summary>
public enum GateWalletDepositStatus : byte
{
    /// <summary>
    /// Deposit blocked
    /// </summary>
    [Map("BLOCKED")]
    Blocked = 1,

    /// <summary>
    /// Deposit credited, withdrawal pending unlock
    /// </summary>
    [Map("DEP_CREDITED")]
    CreditedPendingUnlock = 2,

    /// <summary>
    /// Funds credited to the spot account
    /// </summary>
    [Map("DONE")]
    Done = 3,

    /// <summary>
    /// Invalid transaction
    /// </summary>
    [Map("INVALID")]
    Invalid = 4,

    /// <summary>
    /// Manual review required
    /// </summary>
    [Map("MANUAL")]
    ManualReviewRequired = 5,

    /// <summary>
    /// Processing
    /// </summary>
    [Map("PEND")]
    Processing = 6,

    /// <summary>
    /// Under compliance review
    /// </summary>
    [Map("REVIEW")]
    UnderComplianceReview = 7,

    /// <summary>
    /// Tracking block confirmations, pending spot account credit
    /// </summary>
    [Map("TRACK")]
    TrackingConfirmations = 8,
}
