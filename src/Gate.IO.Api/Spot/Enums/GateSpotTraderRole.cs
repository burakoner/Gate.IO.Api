﻿namespace Gate.IO.Api.Spot;

/// <summary>
/// Trade role. No value in public endpoints
/// </summary>
public enum GateSpotTraderRole : byte
{
    /// <summary>
    /// Enum Taker for value: taker
    /// </summary>
    [Map("taker")]
    Taker = 1,

    /// <summary>
    /// Enum Maker for value: maker
    /// </summary>
    [Map("maker")]
    Maker = 2
}
