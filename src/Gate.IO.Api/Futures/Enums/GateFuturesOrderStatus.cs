namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderStatus
/// </summary>
public enum GateFuturesOrderStatus
{
    /// <summary>
    /// Enum Open for value: open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Enum Finished for value: finished
    /// </summary>
    [Map("finished")]
    Finished = 2,
}
