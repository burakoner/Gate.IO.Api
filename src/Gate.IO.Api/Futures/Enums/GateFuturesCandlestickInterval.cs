namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Candlestick Interval
/// </summary>
public enum GateFuturesCandlestickInterval
{
    /// <summary>
    /// 10 Seconds
    /// </summary>
    [Map("10s")]
    TenSeconds = 10,

    /// <summary>
    /// 1 Minute
    /// </summary>
    [Map("1m")]
    OneMinute = 60,

    /// <summary>
    /// 5 Minutes
    /// </summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>
    /// 15 Minutes
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>
    /// 30 Minutes
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>
    /// 1 Hour
    /// </summary>
    [Map("1h")]
    OneHour = 3600,

    /// <summary>
    /// 4 Hours
    /// </summary>
    [Map("4h")]
    FourHours = 14400,

    /// <summary>
    /// 8 Hours
    /// </summary>
    [Map("8h")]
    EightHours = 28800,

    /// <summary>
    /// 1 Day
    /// </summary>
    [Map("1d")]
    OneDay = 86400,

    /// <summary>
    /// 7 Days
    /// </summary>
    [Map("7d")]
    OneWeek = 604800,

    /// <summary>
    /// 1 Month
    /// </summary>
    [Map("30d")]
    OneMonth = 2592000,
}