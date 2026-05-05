namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx position side
/// </summary>
public enum GateCrossExPositionSide
{
    [Map("NONE")]
    None = 0,

    [Map("LONG")]
    Long = 1,

    [Map("SHORT")]
    Short = 2,
}
