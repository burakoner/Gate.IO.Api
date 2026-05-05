namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P order side
/// </summary>
public enum GateP2pOrderSide : byte
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("sell")]
    Sell = 2,
}
