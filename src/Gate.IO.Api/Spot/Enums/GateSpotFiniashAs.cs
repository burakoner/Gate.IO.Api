namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotFiniashAs
/// </summary>
public enum GateSpotFiniashAs
{
    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled,

    /// <summary>
    /// LiquidateCancelled
    /// </summary>
    [Map("liquidate_cancelled")]
    LiquidateCancelled,

    /// <summary>
    /// DepthNotEnough
    /// </summary>
    [Map("depth_not_enough")]
    DepthNotEnough,

    /// <summary>
    /// TraderNotEnough
    /// </summary>
    [Map("trader_not_enough")]
    TraderNotEnough,
}