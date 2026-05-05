namespace Gate.IO.Api.Margin;

/// <summary>
/// Isolated margin borrow or repay request
/// </summary>
public record GateMarginLoanRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Borrow or repay
    /// </summary>
    public GateMarginUniOrderType Type { get; set; }

    /// <summary>
    /// Borrow or repayment amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Full repayment
    /// </summary>
    public bool? RepaidAll { get; set; }
}
