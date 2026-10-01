namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Perpetual Settlement
/// </summary>
public enum GateFuturesSettlement : byte
{
    /// <summary>
    /// BTC
    /// </summary>
    [Map("btc")]
    BTC = 1,

    // [Map("usd")]
    // USD = 2,

    /// <summary>
    /// USDT
    /// </summary>
    [Map("usdt")]
    USDT = 3,

    /// <summary>
    /// USD1 perpetual futures REST settlement. This does not add WebSocket or DeFi Futures support.
    /// </summary>
    [Map("usd1")]
    USD1 = 4,
}
