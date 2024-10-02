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
    Open,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finished")]
    Finished,
}