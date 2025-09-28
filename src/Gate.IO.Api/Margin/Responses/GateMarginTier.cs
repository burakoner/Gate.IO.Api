namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Lending Tier
/// </summary>
public record GateMarginTier
{
    /// <summary>
    /// Maximum loan limit
    /// </summary>
    [JsonProperty("upper_limit")]
    public decimal UpperLimit { get; set; }

    /// <summary>
    /// Maintenance margin rate
    /// </summary>
    [JsonProperty("mmr")]
    public decimal MMR { get; set; }

    /// <summary>
    /// Maximum leverage multiple
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
}
