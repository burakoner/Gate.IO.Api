namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment product status.
/// </summary>
public enum GateEarnDualPlanStatus : byte
{
    /// <summary>
    /// Not started.
    /// </summary>
    [Map("NOTSTARTED")]
    NotStarted = 1,

    /// <summary>
    /// In progress.
    /// </summary>
    [Map("ONGOING")]
    Ongoing = 2,

    /// <summary>
    /// Ended.
    /// </summary>
    [Map("ENDED")]
    Ended = 3,
}
