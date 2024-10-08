namespace Gate.IO.Api.SubAccount;

/// <summary>
/// SubAccount State
/// </summary>
public enum GateSubAccountState
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("1", "normal")]
    Normal = 1,

    /// <summary>
    /// Locked
    /// </summary>
    [Map("2", "locked")]
    Locked = 2,
}
