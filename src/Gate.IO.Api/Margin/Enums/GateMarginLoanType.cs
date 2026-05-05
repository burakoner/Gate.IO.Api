namespace Gate.IO.Api.Margin;

/// <summary>
/// Isolated margin loan type
/// </summary>
public enum GateMarginLoanType : byte
{
    /// <summary>
    /// Platform borrowing
    /// </summary>
    [Map("platform")]
    Platform = 1,

    /// <summary>
    /// Margin borrowing
    /// </summary>
    [Map("margin")]
    Margin = 2,
}
