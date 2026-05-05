namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P ad status update
/// </summary>
public enum GateP2pAdStatusUpdate : byte
{
    /// <summary>
    /// Listed
    /// </summary>
    Listed = 1,

    /// <summary>
    /// Delisted
    /// </summary>
    Delisted = 3,

    /// <summary>
    /// Closed
    /// </summary>
    Closed = 4,
}
