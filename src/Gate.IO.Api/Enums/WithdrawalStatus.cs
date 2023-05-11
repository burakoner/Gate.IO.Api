namespace Gate.IO.Api.Enums;

public enum WithdrawalStatus
{
    [Label("DONE")]
    Done,

    [Label("CANCEL")]
    Cancelled,

    [Label("REQUEST")]
    Requesting,

    [Label("PEND")]
    Pending,

    [Label("MANUAL")]
    PendingManualApproval,

    [Label("EXTPEND")]
    PendingConfirmAfterSending,

    [Label("BCODE")]
    GateCodeOperation,

    [Label("FAIL")]
    PendingConfirmWhenFail,

    [Label("INVALID")]
    InvalidOrder,

    [Label("VERIFY")]
    Verifying,

    [Label("PROCES")]
    Processing,

    [Label("DMOVE")]
    RequiredManualApproval,

    [Label("SPLITPEND")]
    Splitted,
}
