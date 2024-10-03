namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotOrderUpdateEvent
/// </summary>
public enum GateSpotOrderUpdateEvent
{
    /// <summary>
    /// Put
    /// </summary>
    [Map("put")]
    Put,

    /// <summary>
    /// Update
    /// </summary>
    [Map("update")]
    Update,

    /// <summary>
    /// Finish
    /// </summary>
    [Map("finish")]
    Finish,
}