namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P advertisement trading-limit unit
/// </summary>
public enum GateP2pAdLimitBasis : byte
{
    /// <summary>
    /// Limit by cryptocurrency quantity
    /// </summary>
    Crypto = 0,

    /// <summary>
    /// Limit by fiat amount
    /// </summary>
    Fiat = 1,
}
