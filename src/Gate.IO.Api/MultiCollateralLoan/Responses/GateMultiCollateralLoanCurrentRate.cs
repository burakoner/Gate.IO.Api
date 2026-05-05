namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan current interest rate
/// </summary>
public record GateMultiCollateralLoanCurrentRate
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Current interest rate
    /// </summary>
    [JsonProperty("current_rate")]
    public decimal CurrentRate { get; set; }
}
