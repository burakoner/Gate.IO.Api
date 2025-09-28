namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Lending Status
/// </summary>
public enum GateUnifiedLendingStatus : byte
{
    /// <summary>
    /// Disabled
    /// </summary>
    [Map("disable")]
    Disable = 0,

    /// <summary>
    /// Enabled
    /// </summary>
    [Map("enable")]
    Enable = 1
}
