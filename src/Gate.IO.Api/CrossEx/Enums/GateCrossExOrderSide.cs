namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order side
/// </summary>
public enum GateCrossExOrderSide
{
    /// <summary>
    /// Represents the Buy value.
    /// </summary>
    [Map("BUY")]
    Buy = 1,

    /// <summary>
    /// Represents the Sell value.
    /// </summary>
    [Map("SELL")]
    Sell = 2,
}
