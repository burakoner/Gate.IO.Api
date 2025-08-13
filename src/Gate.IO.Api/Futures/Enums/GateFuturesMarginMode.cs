namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesMarginMode
/// </summary>
public enum GateFuturesMarginMode : byte
{
    /// <summary>
    /// Cross
    /// </summary>
    [Map("CROSS")]
    Cross = 1,

    /// <summary>
    /// Isolated
    /// </summary>
    [Map("ISOLATED")]
    Isolated = 2,
}
