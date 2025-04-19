namespace Gate.IO.Api.Spot;

/// <summary>
/// Ticker timezone
/// </summary>
public enum GateSpotTickerTimezone : byte
{
    /// <summary>
    /// UTC0
    /// </summary>
    [Map("utc0")]
    UTC0 = 0,

    /// <summary>
    /// UTC+8
    /// </summary>
    [Map("utc8")]
    UTC8 = 8,

    /// <summary>
    /// All
    /// </summary>
    [Map("all")]
    All = int.MaxValue
}
