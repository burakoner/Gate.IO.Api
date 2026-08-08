namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx full close position request. The position must be strictly below either the minimum notional amount or the minimum order size,
/// and the account must not have an open order for the symbol.
/// </summary>
public record GateCrossExClosePositionRequest
{
    /// <summary>
    /// Trading pair. Futures and margin symbols are supported.
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Position side. Required for margin positions and optional for futures positions depending on the position mode.
    /// </summary>
    public GateCrossExPositionSide? PositionSide { get; set; }
}
