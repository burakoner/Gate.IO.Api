namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan collateralization ratio
/// </summary>
public record GateMultiCollateralLoanLtv
{
    /// <summary>
    /// Initial collateralization rate
    /// </summary>
    [JsonProperty("init_ltv")]
    public decimal InitialLtv { get; set; }

    /// <summary>
    /// Warning collateralization rate
    /// </summary>
    [JsonProperty("alert_ltv")]
    public decimal AlertLtv { get; set; }

    /// <summary>
    /// Liquidation collateralization rate
    /// </summary>
    [JsonProperty("liquidate_ltv")]
    public decimal LiquidateLtv { get; set; }
}
