﻿namespace Gate.IO.Api.Enums;

/// <summary>
/// Futures Order Time-In-Force
/// </summary>
public enum FuturesTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [Map("gtc")]
    GoodTillCancelled,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel,

    /// <summary>
    /// FillOrKill, fill either completely or none Only. ioc and fok are supported when type=market
    /// </summary>
    [Map("fok")]
    FillOrKill,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [Map("poc")]
    PendingOrCancelled,
}