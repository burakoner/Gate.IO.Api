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

    /// <summary>
    /// USD
    /// </summary>
    [Map("usd")]
    USD = 2,

    /// <summary>
    /// USDT
    /// </summary>
    [Map("usdt")]
    USDT = 3,
}
