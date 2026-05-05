namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified interest deduction status
/// </summary>
public enum GateUnifiedInterestStatus : byte
{
    /// <summary>
    /// Failed
    /// </summary>
    [Map("0", "fail")]
    Failed = 0,

    /// <summary>
    /// Success
    /// </summary>
    [Map("1", "success")]
    Success = 1,
}
