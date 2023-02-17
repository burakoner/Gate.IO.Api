namespace Gate.IO.Api.Enums;

public enum SpotPriceTriggerStatus
{
    [EnumMember(Value = "open")]
    Open,

    [EnumMember(Value = "cancelled")]
    Cancelled,

    [EnumMember(Value = "finish")]
    Finished,

    [EnumMember(Value = "failed")]
    Failed,

    [EnumMember(Value = "expired")]
    Expired,
}
