namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotOrderUpdateEvent
/// </summary>
public enum GateSpotOrderUpdateEvent : byte
{
    /// <summary>
    /// Put
    /// </summary>
    [Map("put")]
    Put = 1,

    /// <summary>
    /// Update
    /// </summary>
    [Map("update")]
    Update = 2,

    /// <summary>
    /// Finish
    /// </summary>
    [Map("finish")]
    Finish = 3,
}