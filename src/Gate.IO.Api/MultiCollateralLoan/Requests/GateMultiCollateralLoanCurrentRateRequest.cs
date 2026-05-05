namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan current rate request
/// </summary>
public record GateMultiCollateralLoanCurrentRateRequest
{
    /// <summary>
    /// Currency names, maximum 100
    /// </summary>
    public IEnumerable<string> Currencies { get; set; }

    /// <summary>
    /// VIP level. Defaults to 0 if not specified.
    /// </summary>
    public string VipLevel { get; set; }
}
