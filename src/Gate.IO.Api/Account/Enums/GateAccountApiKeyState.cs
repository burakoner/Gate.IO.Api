namespace Gate.IO.Api.Account;

/// <summary>
/// Account API key state
/// </summary>
public enum GateAccountApiKeyState : byte
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("1")]
    Normal = 1,

    /// <summary>
    /// Locked
    /// </summary>
    [Map("2")]
    Locked = 2,

    /// <summary>
    /// Frozen
    /// </summary>
    [Map("3")]
    Frozen = 3,
}
