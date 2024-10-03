namespace Gate.IO.Api.Margin;

/// <summary>
/// GateCrossMarginLoanRepaymentType
/// </summary>
public enum GateCrossMarginRepaymentType
{
    /// <summary>
    /// no repayment type
    /// </summary>
    [Map("none")]
    None,
    
    /// <summary>
    /// automatic repayment
    /// </summary>
    [Map("auto_repay")]
    AutoRepayment,

    /// <summary>
    /// manual repayment
    /// </summary>
    [Map("manual_repay")]
    ManualRepayment,

    /// <summary>
    /// automatic repayment after cancellation
    /// </summary>
    [Map("cancel_auto_repay")]
    AutomaticRepaymentAfterCancellation,
}
