namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesStatsInterval
/// </summary>
public enum GateFuturesStatsInterval : byte
{
    /// <summary>
    /// FiveMinutes
    /// </summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>
    /// FifteenMinutes
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>
    /// ThirtyMinutes
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>
    /// OneHour
    /// </summary>
    [Map("1h")]
    OneHour = 3600,

    /// <summary>
    /// FourHours
    /// </summary>
    [Map("4h")]
    FourHours = 14400,

    /// <summary>
    /// OneDay
    /// </summary>
    [Map("1d")]
    OneDay = 86400,
}