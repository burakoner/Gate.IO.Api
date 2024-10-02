namespace Gate.IO.Api.Spot;

/// <summary>
/// Ticker timezone
/// </summary>
public enum GateSpotTickerTimezone
{
    /// <summary>
    /// UTC0
    /// </summary>
    [Map("utc0")]
    UTC0,

    /// <summary>
    /// UTC+8
    /// </summary>
    [Map("utc8")]
    UTC8,

    /// <summary>
    /// All
    /// </summary>
    [Map("all")]
    All
}
