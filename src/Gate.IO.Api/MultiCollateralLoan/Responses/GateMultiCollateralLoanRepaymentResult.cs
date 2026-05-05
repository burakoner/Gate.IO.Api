namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan repayment result
/// </summary>
public record GateMultiCollateralLoanRepaymentResult
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Repaid currencies
    /// </summary>
    [JsonProperty("repaid_currencies")]
    public List<GateMultiCollateralLoanRepaymentCurrencyResult> RepaidCurrencies { get; set; } = [];
}

/// <summary>
/// Multi-collateral loan repayment currency result
/// </summary>
public record GateMultiCollateralLoanRepaymentCurrencyResult
{
    /// <summary>
    /// Whether the repayment was successful
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
    /// Repayment currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Principal
    /// </summary>
    [JsonProperty("repaid_principal")]
    public decimal RepaidPrincipal { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("repaid_interest")]
    public decimal RepaidInterest { get; set; }
}
