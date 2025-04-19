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
    Splitted = 13,
}
