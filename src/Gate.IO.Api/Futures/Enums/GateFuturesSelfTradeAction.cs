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