namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPositionMode
/// </summary>
public enum GateFuturesPositionMode : byte
{
    /// <summary>
    /// Enum Single for value: single
    /// </summary>
    [Map("single")]
    Single = 1,

    /// <summary>
    /// Enum Duallong for value: dual_long
    /// </summary>
    [Map("dual_long")]
    DualLong = 2,

    /// <summary>
    /// Enum Dualshort for value: dual_short
    /// </summary>
    [Map("dual_short")]
    DualShort = 3,
}
