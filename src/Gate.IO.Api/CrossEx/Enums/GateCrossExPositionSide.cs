namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx position side
/// </summary>
public enum GateCrossExPositionSide
{
    /// <summary>
    /// Represents the None value.
    /// </summary>
    [Map("NONE")]
    None = 0,

    /// <summary>
    /// Represents the Long value.
    /// </summary>
    [Map("LONG")]
    Long = 1,

    /// <summary>
    /// Represents the Short value.
    /// </summary>
    [Map("SHORT")]
    Short = 2,
}
