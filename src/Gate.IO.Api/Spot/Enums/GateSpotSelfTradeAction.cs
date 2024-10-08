namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotSelfTradingPreventionAction
/// </summary>
public enum GateSpotSelfTradeAction
{
    /// <summary>
    /// CancelNewest
    /// </summary>
    [Map("cn")]
    CancelNewest = 1,

    /// <summary>
    /// CancelOldest
    /// </summary>
    [Map("co")]
    CancelOldest = 2,

    /// <summary>
    /// CancelBoth
    /// </summary>
    [Map("cb")]
    CancelBoth = 3,
}