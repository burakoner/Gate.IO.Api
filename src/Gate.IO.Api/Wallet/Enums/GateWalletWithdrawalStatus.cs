namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Withdrawal Status
/// </summary>
public enum GateWalletWithdrawalStatus
{
    /// <summary>
    /// Done
    /// </summary>
    [Map("DONE")]
    Done,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("CANCEL")]
    Cancelled,

    /// <summary>
    /// Requesting
    /// </summary>
    [Map("REQUEST")]
    Requesting,

    /// <summary>
    /// Pending Manual Approval
    /// </summary>
    [Map("MANUAL")]
    PendingManualApproval,

    /// <summary>
    /// Gate Code Operation
    /// </summary>
    [Map("BCODE")]
    GateCodeOperation,

    /// <summary>
    /// Pending Confirm After Sending
    /// </summary>
    [Map("EXTPEND")]
    PendingConfirmAfterSending,

    /// <summary>
    /// Pending Confirm When Fail
    /// </summary>
    [Map("FAIL")]
    PendingConfirmWhenFail,

    /// <summary>
    /// Invalid Order
    /// </summary>
    [Map("INVALID")]
    InvalidOrder,

    /// <summary>
    /// Verifying
    /// </summary>
    [Map("VERIFY")]
    Verifying,

    /// <summary>
    /// Processing
    /// </summary>
    [Map("PROCES")]
    Processing,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("PEND")]
    Pending,

    /// <summary>
    /// Pending Confirm
    /// </summary>
    [Map("DMOVE")]
    RequiredManualApproval,

    /// <summary>
    /// Pending Confirm After Split
    /// </summary>
    [Map("SPLITPEND")]
    Splitted,
}
