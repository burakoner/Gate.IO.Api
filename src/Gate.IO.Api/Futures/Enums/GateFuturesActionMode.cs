namespace Gate.IO.Api.Futures;

/// <summary>
/// Controls how much order data is returned after a Futures order operation.
/// </summary>
public enum GateFuturesActionMode : byte
{
    /// <summary>
    /// Asynchronous mode which returns only key order fields.
    /// </summary>
    [Map("ACK")]
    Acknowledge = 1,

    /// <summary>
    /// Returns the order result without clearing information.
    /// </summary>
    [Map("RESULT")]
    Result = 2,

    /// <summary>
    /// Returns the complete order result. This is the default mode.
    /// </summary>
    [Map("FULL")]
    Full = 3,
}
