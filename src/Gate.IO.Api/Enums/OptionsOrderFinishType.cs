namespace Gate.IO.Api.Enums;

/// <summary>
/// How the order was finished.
/// </summary>
public enum OptionsOrderFinishType
{
    /// <summary>
    /// Enum Filled for value: filled
    /// </summary>
    [Label("filled")]
    Filled,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [Label("cancelled")]
    Cancelled,

    /// <summary>
    /// Enum Liquidated for value: liquidated
    /// </summary>
    [Label("liquidated")]
    Liquidated,

    /// <summary>
    /// Enum Ioc for value: ioc
    /// </summary>
    [Label("ioc")]
    IOC,

    /// <summary>
    /// Enum Autodeleveraged for value: auto_deleveraged
    /// </summary>
    [Label("auto_deleveraged")]
    AutoDeleveraged,

    /// <summary>
    /// Enum Reduceonly for value: reduce_only
    /// </summary>
    [Label("reduce_only")]
    ReduceOnly,

    /// <summary>
    /// Enum Positionclosed for value: position_closed
    /// </summary>
    [Label("position_closed")]
    PositionClosed,

    /// <summary>
    /// Enum Reduceout for value: reduce_out
    /// </summary>
    [Label("reduce_out")]
    ReduceOut
}
