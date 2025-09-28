namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Loan Margin Tier
/// </summary>
public record GateUnifiedLoanMarginTiers
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Tiered margin
    /// </summary>
    [JsonProperty("margin_tiers")]
    public List<GateUnifiedLoanMarginTier> DiscountTiers { get; set; } = [];
}

/// <summary>
/// Gate Unified Currency Discount Tier
/// </summary>
public record GateUnifiedLoanMarginTier
{
    /// <summary>
    /// Tier
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Margin Rate
    /// </summary>
    [JsonProperty("margin_rate")]
    public decimal MarginRate { get; set; }

    /// <summary>
    /// Lower Limit
    /// </summary>
    [JsonProperty("lower_limit")]
    public decimal LowerLimit { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Upper Limit
    /// </summary>
    [JsonProperty("upper_limit")]
    public decimal UpperLimit { get; set; }
}
