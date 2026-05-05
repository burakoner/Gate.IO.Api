namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan currency amount
/// </summary>
public record GateMultiCollateralLoanCurrencyAmount
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public decimal Amount { get; set; }
}
