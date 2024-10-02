namespace Gate.IO.Api.Enums;

public enum FuturesCandlestickInterval
{
    [Map("10s")]
    TenSeconds = 10,

    [Map("1m")]
    OneMinute = 60,

    [Map("5m")]
    FiveMinutes = 300,

    [Map("15m")]
    FifteenMinutes = 900,

    [Map("30m")]
    ThirtyMinutes = 1800,

    [Map("1h")]
    OneHour = 3600,

    [Map("4h")]
    FourHours = 14400,

    [Map("8h")]
    EightHours = 28800,

    [Map("1d")]
    OneDay = 86400,

    [Map("7d")]
    OneWeek = 604800,

    [Map("30d")]
    OneMonth = 2592000,
}