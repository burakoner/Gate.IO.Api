namespace Gate.IO.Api.Enums;

public enum FuturesCandlestickInterval
{
    [EnumMember(Value = "10s")]
    TenSeconds = 10,

    [EnumMember(Value = "1m")]
    OneMinute = 60,

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

    [EnumMember(Value = "8h")]
    EightHours = 28800,

    [EnumMember(Value = "1d")]
    OneDay = 86400,

    [EnumMember(Value = "7d")]
    OneWeek = 604800,

    [EnumMember(Value = "30d")]
    OneMonth = 2592000,
}