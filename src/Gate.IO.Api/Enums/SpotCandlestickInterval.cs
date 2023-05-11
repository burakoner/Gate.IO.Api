namespace Gate.IO.Api.Enums;

public enum SpotCandlestickInterval
{
    [Label("10s")]
    TenSeconds = 10,

    [Label("1m")]
    OneMinute = 60,

    [Label("5m")]
    FiveMinutes = 300,

    [Label("15m")]
    FifteenMinutes = 900,

    [Label("30m")]
    ThirtyMinutes = 1800,

    [Label("1h")]
    OneHour = 3600,

    [Label("4h")]
    FourHours = 14400,

    [Label("8h")]
    EightHours = 28800,

    [Label("1d")]
    OneDay = 86400,

    [Label("7d")]
    OneWeek = 604800,

    [Label("30d")]
    OneMonth = 2592000,
}