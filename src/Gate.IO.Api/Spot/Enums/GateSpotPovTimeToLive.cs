namespace Gate.IO.Api.Spot;

/// <summary>
/// Time to live for a Spot POV order
/// </summary>
public enum GateSpotPovTimeToLive : byte
{
    /// <summary>
    /// One hour
    /// </summary>
    [Map("1h")]
    OneHour = 1,

    /// <summary>
    /// Six hours
    /// </summary>
    [Map("6h")]
    SixHours = 2,

    /// <summary>
    /// Twelve hours
    /// </summary>
    [Map("12h")]
    TwelveHours = 3,

    /// <summary>
    /// One day
    /// </summary>
    [Map("1d")]
    OneDay = 4,

    /// <summary>
    /// Two days
    /// </summary>
    [Map("2d")]
    TwoDays = 5,

    /// <summary>
    /// Three days
    /// </summary>
    [Map("3d")]
    ThreeDays = 6,

    /// <summary>
    /// Four days
    /// </summary>
    [Map("4d")]
    FourDays = 7,

    /// <summary>
    /// Five days
    /// </summary>
    [Map("5d")]
    FiveDays = 8,

    /// <summary>
    /// Six days
    /// </summary>
    [Map("6d")]
    SixDays = 9,

    /// <summary>
    /// Seven days
    /// </summary>
    [Map("7d")]
    SevenDays = 10,
}
