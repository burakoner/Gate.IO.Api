namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPriceTriggerStatus
/// </summary>
public enum GateFuturesPriceTriggerStatus
{
    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finished")]
    Finished = 2,

    /// <summary>
    /// Inactive
    /// </summary>
    [Map("inactive")]
    Inactive = 3,

    /// <summary>
    /// Invalid
    /// </summary>
    [Map("invalid")]
    Invalid = 4,
}
