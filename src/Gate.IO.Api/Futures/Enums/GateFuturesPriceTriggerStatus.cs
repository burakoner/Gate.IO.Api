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
    Open,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finished")]
    Finished,

    /// <summary>
    /// Inactive
    /// </summary>
    [Map("inactive")]
    Inactive,

    /// <summary>
    /// Invalid
    /// </summary>
    [Map("invalid")]
    Invalid,
}
