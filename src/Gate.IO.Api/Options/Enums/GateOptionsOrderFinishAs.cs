namespace Gate.IO.Api.Options;

/// <summary>
/// How the order was finished.
/// </summary>
public enum GateOptionsOrderFinishAs
{
    /// <summary>
    /// Enum Filled for value: filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled,

    /// <summary>
    /// Enum Liquidated for value: liquidated
    /// </summary>
    [Map("liquidated")]
    Liquidated,

    /// <summary>
    /// Enum Ioc for value: ioc
    /// </summary>
    [Map("ioc")]
    IOC,

    /// <summary>
    /// Enum Autodeleveraged for value: auto_deleveraged
    /// </summary>
    [Map("auto_deleveraged")]
    AutoDeleveraged,

    /// <summary>
    /// Enum Reduceonly for value: reduce_only
    /// </summary>
    [Map("reduce_only")]
    ReduceOnly,

    /// <summary>
    /// Enum Positionclosed for value: position_closed
    /// </summary>
    [Map("position_closed")]
    PositionClosed,

    /// <summary>
    /// Enum Reduceout for value: reduce_out
    /// </summary>
    [Map("reduce_out")]
    ReduceOut,
    
    /// <summary>
    /// MmpCancelled
    /// </summary>
    [Map("mmp_cancelled")]
    MmpCancelled
}
