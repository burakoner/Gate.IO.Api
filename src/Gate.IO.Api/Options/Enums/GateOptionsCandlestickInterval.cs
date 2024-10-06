namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsCandlestickInterval
/// </summary>
public enum GateOptionsCandlestickInterval
{
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
}