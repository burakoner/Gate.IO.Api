namespace Gate.IO.Api.Spot;

/// <summary>
/// Price trigger filter
/// </summary>
public enum GateSpotTriggerFilter
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