namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesSelfTradingPreventionAction
/// </summary>
public enum GateFuturesSelfTradeAction
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