namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures position margin mode
/// </summary>
public enum GateFuturesPositionMarginMode : byte
{
    /// <summary>
    /// Isolated margin
    /// </summary>
    [Map("isolated")]
    Isolated = 1,

    /// <summary>
    /// Cross margin
    /// </summary>
    [Map("cross")]
    Cross = 2,
}
