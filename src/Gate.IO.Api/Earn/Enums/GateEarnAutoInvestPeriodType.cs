namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest period type
/// </summary>
public enum GateEarnAutoInvestPeriodType : byte
{
    /// <summary>
    /// Daily
    /// </summary>
    [Map("daily")]
    Daily = 1,

    /// <summary>
    /// Weekly
    /// </summary>
    [Map("weekly")]
    Weekly = 2,

    /// <summary>
    /// Biweekly
    /// </summary>
    [Map("biweekly")]
    Biweekly = 3,

    /// <summary>
    /// Monthly
    /// </summary>
    [Map("monthly")]
    Monthly = 4,

    /// <summary>
    /// Hourly
    /// </summary>
    [Map("hourly")]
    Hourly = 5,

    /// <summary>
    /// Every four hours
    /// </summary>
    [Map("4-hourly")]
    FourHourly = 6,
}
