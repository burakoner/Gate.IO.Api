namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha order side.
/// </summary>
public enum GateAlphaOrderSide
{
    /// <summary>
    /// Buy order.
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Sell order.
    /// </summary>
    [Map("sell")]
    Sell = 2,
}
