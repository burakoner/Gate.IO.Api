namespace Gate.IO.Api.Futures;

/// <summary>
/// Set side to close dual-mode position. &#x60;close_long&#x60; closes the long side; while &#x60;close_short&#x60; the short one. Note &#x60;size&#x60; also needs to be set to 0
/// </summary>
public enum GateFuturesOrderAutoSize
{
    /// <summary>
    /// Enum Long for value: close_long
    /// </summary>
    [Map("close_long")]
    CloseLong,

    /// <summary>
    /// Enum Short for value: close_short
    /// </summary>
    [Map("close_short")]
    CloseShort,
}