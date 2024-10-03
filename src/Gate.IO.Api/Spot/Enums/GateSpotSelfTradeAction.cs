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
    CancelNewest,

    /// <summary>
    /// CancelOldest
    /// </summary>
    [Map("co")]
    CancelOldest,

    /// <summary>
    /// CancelBoth
    /// </summary>
    [Map("cb")]
    CancelBoth,
}