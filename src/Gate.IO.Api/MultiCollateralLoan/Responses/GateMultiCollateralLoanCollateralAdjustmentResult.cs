namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan collateral adjustment result
/// </summary>
public record GateMultiCollateralLoanCollateralAdjustmentResult
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Collateral currency information
    /// </summary>
    [JsonProperty("collateral_currencies")]
    public List<GateMultiCollateralLoanCollateralAdjustmentCurrency> CollateralCurrencies { get; set; } = [];
}

/// <summary>
/// Multi-collateral loan collateral adjustment currency result
/// </summary>
public record GateMultiCollateralLoanCollateralAdjustmentCurrency
{
    /// <summary>
    /// Update success status
    /// </summary>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Error identifier for failed operations
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// Error description for failed operations
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Successfully operated collateral quantity
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
