namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsOrderStatus
/// </summary>
public enum GateOptionsOrderStatus : byte
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
