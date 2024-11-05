﻿namespace Gate.IO.Api.Options;

/// <summary>
/// Trade role. No value in public endpoints
/// </summary>
public enum GateOptionsTraderRole
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