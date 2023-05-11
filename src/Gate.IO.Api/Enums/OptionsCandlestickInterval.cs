namespace Gate.IO.Api.Enums;

public enum OptionsCandlestickInterval
{
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
}