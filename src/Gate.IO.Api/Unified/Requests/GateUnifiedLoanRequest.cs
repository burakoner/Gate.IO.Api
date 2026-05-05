namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified borrow or repay request
/// </summary>
public record GateUnifiedLoanRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Borrow or repay
    /// </summary>
    public GateUnifiedLoanDirection Type { get; set; }

    /// <summary>
    /// Borrow or repayment amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Full repayment
    /// </summary>
    public bool? RepaidAll { get; set; }

    /// <summary>
    /// User defined custom ID
    /// </summary>
    public string Text { get; set; }
}
