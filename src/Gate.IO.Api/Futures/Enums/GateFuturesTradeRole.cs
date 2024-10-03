﻿namespace Gate.IO.Api.Futures;

/// <summary>
/// Trade role. No value in public endpoints
/// </summary>
public enum GateFuturesTradeRole
{
    /// <summary>
    /// Enum Taker for value: taker
    /// </summary>
    [Map("taker")]
    Taker,

    /// <summary>
    /// Enum Maker for value: maker
    /// </summary>
    [Map("maker")]
    Maker
}