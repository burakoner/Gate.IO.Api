namespace Gate.IO.Api.Enums;

public enum DeliveryCycle
{
    [EnumMember(Value = "WEEKLY")]
    Weekly,

    [EnumMember(Value = "BI-WEEKLY")]
    BiWeekly,

    [EnumMember(Value = "QUARTERLY")]
    Quarterly,

    [EnumMember(Value = "BI-QUARTERLY")]
    BiQuarterly,
}