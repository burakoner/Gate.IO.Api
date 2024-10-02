namespace Gate.IO.Api.Enums;

public enum OptionsCandlestickInterval
{
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
}