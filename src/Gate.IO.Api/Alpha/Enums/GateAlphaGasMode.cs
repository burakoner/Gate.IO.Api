namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha trading mode that controls slippage selection.
/// </summary>
public enum GateAlphaGasMode
{
    /// <summary>
    /// Smart mode.
    /// </summary>
    [Map("speed")]
    Speed = 1,

    /// <summary>
    /// Custom mode; uses the slippage parameter.
    /// </summary>
    [Map("custom")]
    Custom = 2,
}
