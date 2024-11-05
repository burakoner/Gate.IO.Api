﻿namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsSide
/// </summary>
public enum GateOptionsSide
{
    /// <summary>
    /// Long
    /// </summary>
    [Map("long")]
    Long = 1,

    /// <summary>
    /// Short
    /// </summary>
    [Map("short")]
    Short = 2,
}