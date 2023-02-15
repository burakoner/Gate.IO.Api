namespace Gate.IO.Api.Enums;

public enum TickerTimezone
{
    [EnumMember(Value = "utc0")]
    UTC0 = 0,

    [EnumMember(Value = "utc8")]
    UTC8 = 8,

    [EnumMember(Value = "all")]
    All = -1
}
