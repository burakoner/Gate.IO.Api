namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order position side filter
/// </summary>
public enum GateFuturesChaseOrderSide : byte
{
    /// <summary>Explicit unknown side filter, as documented by the current endpoint.</summary>
    Unknown = 0,

    /// <summary>
    /// Long side
    /// </summary>
    Long = 1,

    /// <summary>
    /// Short side
    /// </summary>
    Short = 2,
}
