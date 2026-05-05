namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni currency detail
/// </summary>
public record GateEarnUniCurrency
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Minimum lending amount
    /// </summary>
    [JsonProperty("min_lend_amount")]
    public decimal MinimumLendAmount { get; set; }

    /// <summary>
    /// Total maximum lending amount, in USDT
    /// </summary>
    [JsonProperty("max_lend_amount")]
    public decimal MaximumLendAmount { get; set; }

    /// <summary>
    /// Maximum hourly rate
    /// </summary>
    [JsonProperty("max_rate")]
    public decimal MaximumRate { get; set; }

    /// <summary>
    /// Minimum hourly rate
    /// </summary>
    [JsonProperty("min_rate")]
    public decimal MinimumRate { get; set; }
}
