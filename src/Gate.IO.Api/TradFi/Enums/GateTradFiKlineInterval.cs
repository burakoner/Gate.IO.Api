namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi kline interval
/// </summary>
public enum GateTradFiKlineInterval
{
    /// <summary>
    /// Represents the One Minute value.
    /// </summary>
    [Map("1m")]
    OneMinute = 1,

    /// <summary>
    /// Represents the Fifteen Minutes value.
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 2,

    /// <summary>
    /// Represents the One Hour value.
    /// </summary>
    [Map("1h")]
    OneHour = 3,

    /// <summary>
    /// Represents the Four Hours value.
    /// </summary>
    [Map("4h")]
    FourHours = 4,

    /// <summary>
    /// Represents the One Day value.
    /// </summary>
    [Map("1d")]
    OneDay = 5,

    /// <summary>
    /// Represents the Seven Days value.
    /// </summary>
    [Map("7d")]
    SevenDays = 6,

    /// <summary>
    /// Represents the Thirty Days value.
    /// </summary>
    [Map("30d")]
    ThirtyDays = 7,
}
