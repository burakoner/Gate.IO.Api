namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotFiniashAs
/// </summary>
public enum GateSpotFinishAs
{
    /// <summary>
    /// Unknown
    /// </summary>
    [Map("unknown")]
    Unknown = 0,

    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 2,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled = 3,

    /// <summary>
    /// LiquidateCancelled
    /// </summary>
    [Map("liquidate_cancelled")]
    LiquidateCancelled = 4,

    /// <summary>
    /// Order quantity too small
    /// </summary>
    [Map("small")]
    Small = 5,

    /// <summary>
    /// DepthNotEnough
    /// </summary>
    [Map("depth_not_enough")]
    DepthNotEnough = 6,

    /// <summary>
    /// TraderNotEnough
    /// </summary>
    [Map("trader_not_enough")]
    TraderNotEnough = 7,

    /// <summary>
    /// Not immediately filled because tif is set to ioc
    /// </summary>
    [Map("ioc")]
    IOC = 8,

    /// <summary>
    /// Not met the order strategy because tif is set to poc
    /// </summary>
    [Map("poc")]
    POC = 9,

    /// <summary>
    /// Not fully filled immediately because tif is set to fok
    /// </summary>
    [Map("fok")]
    FOK = 10,

    /// <summary>
    /// Cancelled due to self-trade prevention
    /// </summary>
    [Map("stp")]
    SelfTradePrevention = 11,
}