namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPositionMode
/// </summary>
public enum GateFuturesPositionMode
{
    /// <summary>
    /// Enum Single for value: single
    /// </summary>
    [Map("single")]
    Single,

    /// <summary>
    /// Enum Duallong for value: dual_long
    /// </summary>
    [Map("dual_long")]
    DualLong,

    /// <summary>
    /// Enum Dualshort for value: dual_short
    /// </summary>
    [Map("dual_short")]
    DualShort,
}
