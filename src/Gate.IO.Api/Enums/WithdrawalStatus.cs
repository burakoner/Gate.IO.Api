namespace Gate.IO.Api.Enums;

public enum WithdrawalStatus
{
    [EnumMember(Value = "DONE")]
    Done,

    [EnumMember(Value = "CANCEL")]
    Cancelled,

    [EnumMember(Value = "REQUEST")]
    Requesting,

    [EnumMember(Value = "PEND")]
    Pending,

    [EnumMember(Value = "MANUAL")]
    PendingManualApproval,

    [EnumMember(Value = "EXTPEND")]
    PendingConfirmAfterSending,

    [EnumMember(Value = "BCODE")]
    GateCodeOperation,

    [EnumMember(Value = "FAIL")]
    PendingConfirmWhenFail,

    [EnumMember(Value = "INVALID")]
    InvalidOrder,

    [EnumMember(Value = "VERIFY")]
    Verifying,

    [EnumMember(Value = "PROCES")]
    Processing,

    [EnumMember(Value = "DMOVE")]
    RequiredManualApproval,

    [EnumMember(Value = "SPLITPEND")]
    Splitted,
}
