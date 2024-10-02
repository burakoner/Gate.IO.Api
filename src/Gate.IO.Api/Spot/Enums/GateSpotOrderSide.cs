namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Side
/// </summary>
public enum GateSpotOrderSide
{
    /// <summary>
    /// Enum Buy for value: buy
    /// </summary>
    [Map("buy")]
    Buy,

    /// <summary>
    /// Enum Sell for value: sell
    /// </summary>
    [Map("sell")]
    Sell
}