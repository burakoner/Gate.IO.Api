namespace Gate.IO.Api.Unified;

/// <summary>
/// Loan type
/// </summary>
public enum GateUnifiedLoanType : byte
{
    /// <summary>
    /// Platform
    /// </summary>
    [Map("platform")]
    Platform = 1,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("margin")]
    Margin = 2
}
