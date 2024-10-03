namespace Gate.IO.Api.Futures;

public enum FuturesStatsInterval
{
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

    [Map("1d")]
    OneDay = 86400,
}