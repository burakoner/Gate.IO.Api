namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account quick repayment request
/// </summary>
public record GateUnifiedQuickRepaymentRequest
{
    /// <summary>
    /// Liability currencies to repay
    /// </summary>
    public IEnumerable<string> DebtCurrencies { get; set; }

    /// <summary>
    /// Currencies to use for repayment
    /// </summary>
    public IEnumerable<string> AvailableCurrencies { get; set; }
}
