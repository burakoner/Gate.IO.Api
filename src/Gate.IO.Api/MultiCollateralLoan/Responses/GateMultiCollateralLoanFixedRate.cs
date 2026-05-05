namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan fixed interest rate
/// </summary>
public record GateMultiCollateralLoanFixedRate
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Fixed interest rate for 7-day lending period
    /// </summary>
    [JsonProperty("rate_7d")]
    public decimal Rate7Days { get; set; }

    /// <summary>
    /// Fixed interest rate for 30-day lending period
    /// </summary>
    [JsonProperty("rate_30d")]
    public decimal Rate30Days { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }
}
