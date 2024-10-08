namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesDeliveryCycle
/// </summary>
public enum GateFuturesDeliveryCycle
{
    /// <summary>
    /// Weekly
    /// </summary>
    [Map("WEEKLY")]
    Weekly = 1,

    /// <summary>
    /// Bi-Weekly
    /// </summary>
    [Map("BI-WEEKLY")]
    BiWeekly = 2,

    /// <summary>
    /// Quarterly
    /// </summary>
    [Map("QUARTERLY")]
    Quarterly = 3,

    /// <summary>
    /// Bi-Quarterly
    /// </summary>
    [Map("BI-QUARTERLY")]
    BiQuarterly = 4,
}