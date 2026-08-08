namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order price type
/// </summary>
public enum GateFuturesChaseOrderPriceType : byte
{
    /// <summary>
    /// Follow the best bid or ask price
    /// </summary>
    BestPrice = 1,

    /// <summary>
    /// Keep a configured distance from the best bid or ask price
    /// </summary>
    PriceGap = 2,
}
