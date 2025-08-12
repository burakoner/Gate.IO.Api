namespace Gate.IO.Api.Margin;

/// <summary>
/// GateCrossMarginLoanRepaymentType
/// </summary>
public enum GateMarginRepaymentType:byte
{
    /// <summary>
    /// no repayment type
    /// </summary>
    [Map("none")]
    None = 0,
    
    /// <summary>
    /// automatic repayment
    /// </summary>
    [Map("auto_repay")]
    AutoRepayment = 1,

    /// <summary>
    /// manual repayment
    /// </summary>
    [Map("manual_repay")]
    ManualRepayment = 2,

    /// <summary>
    /// automatic repayment after cancellation
    /// </summary>
    [Map("cancel_auto_repay")]
    AutomaticRepaymentAfterCancellation = 3,
}
