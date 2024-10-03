namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotFiniashAs
/// </summary>
public enum GateSpotFinishAs
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
    /// Order quantity too small
    /// </summary>
        [Map("small")]
    Small,
        
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

    /// <summary>
    /// Not immediately filled because tif is set to ioc
    /// </summary>
    [Map("ioc")]
    IOC,

    /// <summary>
    /// Not met the order strategy because tif is set to poc
    /// </summary>
    [Map("poc")]
    POC,

    /// <summary>
    /// Not fully filled immediately because tif is set to fok
    /// </summary>
    [Map("fok")]
    FOK,

    /// <summary>
    /// Cancelled due to self-trade prevention
    /// </summary>
    [Map("stp")]
    SelfTradePrevention,

    /// <summary>
    /// Unknown
    /// </summary>
    [Map("unknown")]
    Unknown,
}