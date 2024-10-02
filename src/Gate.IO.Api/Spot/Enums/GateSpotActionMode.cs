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
    Acknowledge,

    /// <summary>
    /// No clearing information 
    /// </summary>
    [Map("RESULT")]
    Result,

    /// <summary>
    /// Full mode (default)
    /// </summary>
    [Map("FULL")]
    Full,
}