namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan currency quota request
/// </summary>
public record GateMultiCollateralLoanCurrencyQuotaRequest
{
    /// <summary>
    /// Currency type
    /// </summary>
    public GateMultiCollateralLoanCurrencyQuotaType Type { get; set; }

    /// <summary>
    /// Currencies. Collateral quota accepts multiple currencies; borrow quota accepts one.
    /// </summary>
    public IEnumerable<string> Currencies { get; set; }
}
