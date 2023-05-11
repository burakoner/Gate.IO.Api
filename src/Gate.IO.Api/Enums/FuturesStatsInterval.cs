namespace Gate.IO.Api.Enums;

public enum FuturesStatsInterval
{
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

    [Label("1d")]
    OneDay = 86400,
}