namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx kline stream interval.
/// </summary>
public enum GateCrossExKlineInterval
{
    /// <summary>
    /// One minute interval.
    /// </summary>
    [Map("1m")]
    OneMinute = 1,

    /// <summary>
    /// Five minute interval.
    /// </summary>
    [Map("5m")]
    FiveMinutes = 5,

    /// <summary>
    /// Fifteen minute interval.
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 15,

    /// <summary>
    /// Thirty minute interval.
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 30,

    /// <summary>
    /// One hour interval.
    /// </summary>
    [Map("1h")]
    OneHour = 60,

    /// <summary>
    /// Four hour interval.
    /// </summary>
    [Map("4h")]
    FourHours = 240,

    /// <summary>
    /// One day interval.
    /// </summary>
    [Map("1d")]
    OneDay = 1440,
}
