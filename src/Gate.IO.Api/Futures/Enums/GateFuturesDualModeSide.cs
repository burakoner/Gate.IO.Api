namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesDualModeSide
/// </summary>
public enum GateFuturesDualModeSide : byte
{
    /// <summary>
    /// Dual Long
    /// </summary>
    [Map("dual_long")]
    DualLong = 1,

    /// <summary>
    /// Dual Short
    /// </summary>
    [Map("dual_short")]
    DualShort = 2,
}
