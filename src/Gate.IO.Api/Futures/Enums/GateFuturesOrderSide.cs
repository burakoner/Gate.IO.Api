namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderSide
/// </summary>
public enum GateFuturesOrderSide : byte
{
    /// <summary>
    /// Ask
    /// </summary>
    [Map("ask")]
    Ask = 1,

    /// <summary>
    /// Bid
    /// </summary>
    [Map("bid")]
    Bid = 2,
}
