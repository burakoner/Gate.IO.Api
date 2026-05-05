namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Sub-account API key state
/// </summary>
public enum GateSubAccountApiKeyState : byte
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("1", "normal")]
    Normal = 1,

    /// <summary>
    /// Frozen
    /// </summary>
    [Map("2", "frozen")]
    Frozen = 2,

    /// <summary>
    /// Locked
    /// </summary>
    [Map("3", "locked")]
    Locked = 3,
}
