namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx position mode
/// </summary>
public enum GateCrossExPositionMode
{
    /// <summary>
    /// Represents the Single value.
    /// </summary>
    [Map("SINGLE")]
    Single = 1,

    /// <summary>
    /// Represents the Dual value.
    /// </summary>
    [Map("DUAL")]
    Dual = 2,
}
