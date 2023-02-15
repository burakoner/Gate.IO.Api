namespace Gate.IO.Api.Enums;

public enum FuturesStatsInterval
{
    [EnumMember(Value = "5m")]
    FiveMinutes = 300,

    [EnumMember(Value = "15m")]
    FifteenMinutes = 900,

    [EnumMember(Value = "30m")]
    ThirtyMinutes = 1800,

    [EnumMember(Value = "1h")]
    OneHour = 3600,

    [EnumMember(Value = "4h")]
    FourHours = 14400,

    [EnumMember(Value = "1d")]
    OneDay = 86400,
}