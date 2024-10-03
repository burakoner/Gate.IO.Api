﻿namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesDualModeSide
/// </summary>
public enum GateFuturesDualModeSide
{
    /// <summary>
    /// Dual Long
    /// </summary>
    [Map("dual_long")]
    DualLong,

    /// <summary>
    /// Dual Short
    /// </summary>
    [Map("dual_short")]
    DualShort,
}