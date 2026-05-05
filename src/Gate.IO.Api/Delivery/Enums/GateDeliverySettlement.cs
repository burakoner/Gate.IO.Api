namespace Gate.IO.Api.Delivery;

/// <summary>
/// Gate.IO Futures Delivery Settlement
/// </summary>
public enum GateDeliverySettlement : byte
{
    /// <summary>
    /// BTC
    /// </summary>
    [Map("btc")]
    BTC = 1,

    /// <summary>
    /// USDT
    /// </summary>
    [Map("usdt")]
    USDT = 3,
}
