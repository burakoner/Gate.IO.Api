namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P merchant custom work cycle
/// </summary>
public enum GateP2pMerchantWorkCycle : byte
{
    /// <summary>
    /// Daily
    /// </summary>
    [Map("Daily")]
    Daily = 1,

    /// <summary>
    /// Weekly
    /// </summary>
    [Map("Weekly")]
    Weekly = 2,
}
