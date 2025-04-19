namespace Gate.IO.Api.Delivery;

/// <summary>
/// Gate.IO Futures Delivery Settlement
/// </summary>
public enum GateDeliverySettlement : byte
{
    /// <summary>
    /// USDT
    /// </summary>
    [Map("usdt")]
    USDT = 1,
}
