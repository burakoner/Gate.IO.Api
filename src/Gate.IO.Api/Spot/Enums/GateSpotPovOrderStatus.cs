namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot POV order status
/// </summary>
public enum GateSpotPovOrderStatus : byte
{
    /// <summary>
    /// Created
    /// </summary>
    [Map("CREATED")]
    Created = 1,

    /// <summary>
    /// Cancellation is in progress
    /// </summary>
    [Map("CANCELING")]
    Canceling = 2,

    /// <summary>
    /// Running
    /// </summary>
    [Map("RUNNING")]
    Running = 3,

    /// <summary>
    /// Completed
    /// </summary>
    [Map("COMPLETED")]
    Completed = 4,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("EXPIRED")]
    Expired = 5,

    /// <summary>
    /// Terminated
    /// </summary>
    [Map("TERMINATED")]
    Terminated = 6,
}
