namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotSelfTradingPreventionAction
/// </summary>
public enum GateSpotSelfTradingPreventionAction
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