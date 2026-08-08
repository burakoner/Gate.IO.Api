namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order price gap type
/// </summary>
public enum GateFuturesChaseOrderPriceGapType : byte
{
    /// <summary>
    /// Absolute price gap
    /// </summary>
    Absolute = 1,

    /// <summary>
    /// Percentage price gap
    /// </summary>
    Percentage = 2,
}
