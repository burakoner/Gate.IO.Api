namespace Gate.IO.Api.Enums;

public enum PriceTriggerStatus
{
    [EnumMember(Value = "open")]
    Open = 1,

    [EnumMember(Value = "cancelled")]
    Cancelled = 2,

    [EnumMember(Value = "finish")]
    Finished = 3,

    [EnumMember(Value = "failed")]
    Failed = 4,

    [EnumMember(Value = "expired")]
    Expired = 5,
}
