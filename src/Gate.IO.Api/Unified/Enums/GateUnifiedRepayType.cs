namespace Gate.IO.Api.Unified;

/// <summary>
/// Repay type
/// </summary>
public enum GateUnifiedRepayType : byte
{
    /// <summary>
    /// No repay
    /// </summary>
    [Map("none")]
    None = 0,

    /// <summary>
    /// Manual repayment
    /// </summary>
    [Map("manual_repay")]
    ManualRepay = 1,

    /// <summary>
    /// Automatic repayment
    /// </summary>
    [Map("auto_repay")]
    AutoRepay = 2,

    /// <summary>
    /// Automatic repayment after cancelation
    /// </summary>
    [Map("cancel_auto_repay")]
    CancelAutoRepay = 3
}
