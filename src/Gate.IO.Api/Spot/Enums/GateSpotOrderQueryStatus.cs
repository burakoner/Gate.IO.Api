namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Query Status
/// </summary>
public enum GateSpotOrderQueryStatus
{
    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finished")]
    Finished = 2,
}