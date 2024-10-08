namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderSide
/// </summary>
public enum GateFuturesOrderSide
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
