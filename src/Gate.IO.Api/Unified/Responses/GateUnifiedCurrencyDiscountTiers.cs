namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Currency Discount Tiers
/// </summary>
public record GateUnifiedCurrencyDiscountTiers
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Tiered discount
    /// </summary>
    [JsonProperty("discount_tiers")]
    public List<GateUnifiedCurrencyDiscountTier> DiscountTiers { get; set; } = [];
}

/// <summary>
/// Gate Unified Currency Discount Tier
/// </summary>
public record GateUnifiedCurrencyDiscountTier
{
    /// <summary>
    /// Tier
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Discount
    /// </summary>
    [JsonProperty("discount")]
    public decimal Discount { get; set; }

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
