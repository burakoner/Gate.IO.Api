namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsCandlestickInterval
/// </summary>
public enum GateOptionsCandlestickInterval
{
    /// <summary>
    /// Ten Seconds
    /// </summary>
    [Map("10s")]
    TenSeconds = 10,

    /// <summary>
    /// One Minute
    /// </summary>
    [Map("1m")]
    OneMinute = 60,

    /// <summary>
    /// Five Minutes
    /// </summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>
    /// Fifteen Minutes
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>
    /// Thirty Minutes
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>
    /// One Hour
    /// </summary>
    [Map("1h")]
    OneHour = 3600,

    /// <summary>
    /// Four Hours
    /// </summary>
    [Map("4h")]
    FourHours = 14400,

    /// <summary>
    /// Eight Hours
    /// </summary>
    [Map("8h")]
    EightHours = 28800,

    /// <summary>
    /// One Day
    /// </summary>
    [Map("1d")]
    OneDay = 86400,

    /// <summary>
    /// Seven Days
    /// </summary>
    [Map("7d")]
    SevenDays = 604800,
}
