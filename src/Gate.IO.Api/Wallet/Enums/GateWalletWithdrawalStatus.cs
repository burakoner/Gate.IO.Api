namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Withdrawal Status
/// </summary>
public enum GateWalletWithdrawalStatus : byte
{
    /// <summary>
    /// Done
    /// </summary>
    [Map("DONE")]
    Done = 1,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("CANCEL")]
    Cancelled = 2,

    /// <summary>
    /// Requesting
    /// </summary>
    [Map("REQUEST")]
    Requesting = 3,

    /// <summary>
    /// Pending Manual Approval
    /// </summary>
    [Map("MANUAL")]
    PendingManualApproval = 4,

    /// <summary>
    /// Gate Code Operation
    /// </summary>
    [Map("BCODE")]
    GateCodeOperation = 5,

    /// <summary>
    /// Pending Confirm After Sending
    /// </summary>
    [Map("EXTPEND")]
    PendingConfirmAfterSending = 6,

    /// <summary>
    /// Pending Confirm When Fail
    /// </summary>
    [Map("FAIL")]
    PendingConfirmWhenFail = 7,

    /// <summary>
    /// Invalid Order
    /// </summary>
    [Map("INVALID")]
    InvalidOrder = 8,

    /// <summary>
    /// Verifying
    /// </summary>
    [Map("VERIFY")]
    Verifying = 9,

    /// <summary>
    /// Processing
    /// </summary>
    [Map("PROCES")]
    Processing = 10,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("PEND")]
    Pending = 11,

    /// <summary>
    /// Pending Confirm
    /// </summary>
    [Map("DMOVE")]
    RequiredManualApproval = 12,

    /// <summary>
    /// Pending Confirm After Split
    /// </summary>
    [Map("SPLITPEND")]
    [Obsolete]
    Splitted = 13,

    /// <summary>
    /// Under Review
    /// </summary>
    [Map("REVIEW")]
    UnderReview = 14,

    /// <summary>
    /// Withdrawal Cancellation Pending
    /// </summary>
    [Map("CANCELPEND")]
    CancellationPending = 15,

    /// <summary>
    /// Facial Verification in Progress
    /// </summary>
    [Map("FVERIFY")]
    FacialVerification = 16,

    /// <summary>
    /// Wallet-Side Order Locked
    /// </summary>
    [Map("LOCKED")]
    Locked = 17,

    /// <summary>
    /// Rejected
    /// </summary>
    [Map("REJECT")]
    Rejected = 18,
}
