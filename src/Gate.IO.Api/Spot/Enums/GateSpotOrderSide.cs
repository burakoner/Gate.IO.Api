namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Side
/// </summary>
public enum GateSpotOrderSide : byte
{
    /// <summary>
    /// Enum Buy for value: buy
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Enum Sell for value: sell
    /// </summary>
    [Map("sell")]
    Sell = 2
}