namespace Gate.IO.Api.Enums;

public enum FuturesPriceTriggerStatus
{
    [EnumMember(Value = "open")]
    Open,

    [EnumMember(Value = "finished")]
    Finished,

    [EnumMember(Value = "inactive")]
    Inactive,

    [EnumMember(Value = "invalid")]
    Invalid,
}
