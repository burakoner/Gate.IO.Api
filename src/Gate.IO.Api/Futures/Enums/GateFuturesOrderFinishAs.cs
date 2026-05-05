namespace Gate.IO.Api.Futures;

/// <summary>
/// How the order was finished.
/// </summary>
public enum GateFuturesOrderFinishAs : byte
{
    /// <summary>
    /// Enum Filled for value: filled
    /// </summary>
    [Map("filled")]
    Filled = 1,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled = 2,

    /// <summary>
    /// Enum Liquidated for value: liquidated
    /// </summary>
    [Map("liquidated")]
    Liquidated = 3,

    /// <summary>
    /// Enum Ioc for value: ioc
    /// </summary>
    [Map("ioc")]
    IOC = 4,

    /// <summary>
    /// Enum Autodeleveraged for value: auto_deleveraged
    /// </summary>
    [Map("auto_deleveraged")]
    AutoDeleveraged = 5,

    /// <summary>
    /// Enum Reduceonly for value: reduce_only
    /// </summary>
    [Map("reduce_only")]
    ReduceOnly = 6,

    /// <summary>
    /// Enum Positionclosed for value: position_closed
    /// </summary>
    [Map("position_closed")]
    PositionClosed = 7,

    /// <summary>
    /// Enum Reduceout for value: reduce_out
    /// </summary>
    [Map("reduce_out")]
    ReduceOut = 8,

    /// <summary>
    /// cancelled because self trade prevention
    /// </summary>
    [Map("stp")]
    SelfTradePrevention = 9,
    /// <summary>
    /// Price-triggered order succeeded.
    /// </summary>
    [Map("succeeded")]
    Succeeded = 10,

    /// <summary>
    /// Price-triggered order failed.
    /// </summary>
    [Map("failed")]
    Failed = 11,

    /// <summary>
    /// Price-triggered order expired.
    /// </summary>
    [Map("expired")]
    Expired = 12,

    /// <summary>
    /// Completed by automatic deleveraging.
    /// </summary>
    [Map("auto_deleveraging")]
    AutoDeleveraging = 13,

    /// <summary>
    /// Cancelled due to position closing.
    /// </summary>
    [Map("position_close")]
    PositionClose = 14,

    /// <summary>
    /// Newly created order update.
    /// </summary>
    [Map("_new")]
    New = 15,

    /// <summary>
    /// Order filled, partially filled, or amended.
    /// </summary>
    [Map("_update")]
    Update = 16
}
