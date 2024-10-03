namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Perpetual Settlement
/// </summary>
public enum GateFuturesSettlement
{
    /// <summary>
    /// BTC
    /// </summary>
    [Map("btc")]
    BTC,

    /// <summary>
    /// USD
    /// </summary>
    [Map("usd")]
    USD,

    /// <summary>
    /// USDT
    /// </summary>
    [Map("usdt")]
    USDT,
}
