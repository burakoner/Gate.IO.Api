namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotActionMode
/// </summary>
public enum GateSpotActionMode
{
    /// <summary>
    /// Asynchronous mode, only returns key order fields 
    /// </summary>
    [Map("ACK")]
    Acknowledge = 1,

    /// <summary>
    /// No clearing information 
    /// </summary>
    [Map("RESULT")]
    Result = 2,

    /// <summary>
    /// Full mode (default)
    /// </summary>
    [Map("FULL")]
    Full = 3,
}