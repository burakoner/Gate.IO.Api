namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order attribute
/// </summary>
public enum GateCrossExOrderAttribute
{
    /// <summary>
    /// Normal order.
    /// </summary>
    [Map("COMMON")]
    Common = 1,

    /// <summary>
    /// Liquidation takeover order.
    /// </summary>
    [Map("LIQ")]
    LiquidationTakeover = 2,

    /// <summary>
    /// Liquidation reduction order.
    /// </summary>
    [Map("REDUCE")]
    LiquidationReduction = 3,

    /// <summary>
    /// Auto-deleverage order.
    /// </summary>
    [Map("ADL")]
    AutoDeleverage = 4,

    /// <summary>
    /// Delisting settlement order.
    /// </summary>
    [Map("SETTLEMENT")]
    Settlement = 5,
}
