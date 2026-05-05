namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx risk limit information
/// </summary>
public record GateCrossExRiskLimit
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Tiers.
    /// </summary>
    [JsonProperty("tiers")]
    public List<GateCrossExRiskLimitTier> Tiers { get; set; } = [];
}
