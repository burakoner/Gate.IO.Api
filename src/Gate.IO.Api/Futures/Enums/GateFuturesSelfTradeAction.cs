namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesSelfTradingPreventionAction
/// </summary>
public enum GateFuturesSelfTradeAction : byte
{
    /// <summary>
    /// No self-trading prevention action returned by the API
    /// </summary>
    [Map("-")]
    None = 0,

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
