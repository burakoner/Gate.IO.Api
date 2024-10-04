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
    Weekly,

    /// <summary>
    /// Bi-Weekly
    /// </summary>
    [Map("BI-WEEKLY")]
    BiWeekly,

    /// <summary>
    /// Quarterly
    /// </summary>
    [Map("QUARTERLY")]
    Quarterly,

    /// <summary>
    /// Bi-Quarterly
    /// </summary>
    [Map("BI-QUARTERLY")]
    BiQuarterly,
}