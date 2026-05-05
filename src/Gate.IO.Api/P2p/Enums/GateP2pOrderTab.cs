namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P order tab
/// </summary>
public enum GateP2pOrderTab : byte
{
    /// <summary>
    /// Pending orders
    /// </summary>
    [Map("pending")]
    Pending = 1,

    /// <summary>
    /// Dispute orders
    /// </summary>
    [Map("dispute")]
    Dispute = 2,
}
