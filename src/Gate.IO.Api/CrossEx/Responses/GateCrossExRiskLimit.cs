namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx risk limit information
/// </summary>
public record GateCrossExRiskLimit
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("tiers")]
    public List<GateCrossExRiskLimitTier> Tiers { get; set; } = [];
}
