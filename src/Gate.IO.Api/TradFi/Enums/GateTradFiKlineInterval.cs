namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi kline interval
/// </summary>
public enum GateTradFiKlineInterval
{
    [Map("1m")]
    OneMinute = 1,

    [Map("15m")]
    FifteenMinutes = 2,

    [Map("1h")]
    OneHour = 3,

    [Map("4h")]
    FourHours = 4,

    [Map("1d")]
    OneDay = 5,

    [Map("7d")]
    SevenDays = 6,

    [Map("30d")]
    ThirtyDays = 7,
}
