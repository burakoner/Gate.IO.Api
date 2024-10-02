namespace Gate.IO.Api.Spot;

/// <summary>
/// Candlestick Interval
/// </summary>
public enum GateSpotCandlestickInterval
{
    /// <summary>
    /// 10 seconds
    /// </summary>
    [Map("10s")]
    TenSeconds = 10,

    /// <summary>
    /// 1 minute
    /// </summary>
    [Map("1m")]
    OneMinute = 60,

    /// <summary>
    /// 5 minutes
    /// </summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>
    /// 15 minutes
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>
    /// 30 minutes
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>
    /// 1 hour
    /// </summary>
    [Map("1h")]
    OneHour = 3600,

    /// <summary>
    /// 4 hours
    /// </summary>
    [Map("4h")]
    FourHours = 14400,

    /// <summary>
    /// 8 hours
    /// </summary>
    [Map("8h")]
    EightHours = 28800,

    /// <summary>
    /// 1 day
    /// </summary>
    [Map("1d")]
    OneDay = 86400,

    /// <summary>
    /// 7 days
    /// </summary>
    [Map("7d")]
    OneWeek = 604800,

    /// <summary>
    /// 30 days
    /// </summary>
    [Map("30d")]
    OneMonth = 2592000,
}