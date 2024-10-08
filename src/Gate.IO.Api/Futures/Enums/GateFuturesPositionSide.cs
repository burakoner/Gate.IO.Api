namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPositionSide
/// </summary>
public enum GateFuturesPositionSide
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
