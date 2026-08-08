namespace Gate.IO.Api.Futures;

/// <summary>
/// Hedging status of a futures position under the Delta-neutral strategy
/// </summary>
public enum GateFuturesHedgeStatus : byte
{
    /// <summary>
    /// Position is partially hedged
    /// </summary>
    [Map("partial_hedged")]
    PartiallyHedged = 1,

    /// <summary>
    /// Position is fully hedged
    /// </summary>
    [Map("full_hedged")]
    FullyHedged = 2,
}
