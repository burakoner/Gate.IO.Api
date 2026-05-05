namespace Gate.IO.Api.Account;

/// <summary>
/// Represents the Gate Account Rate Limit.
/// </summary>
public record GateAccountRateLimit
{
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the Tier.
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Gets or sets the Ratio.
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }

    /// <summary>
    /// Gets or sets the Main Ratio.
    /// </summary>
    [JsonProperty("main_ratio")]
    public decimal MainRatio { get; set; }

    /// <summary>
    /// Gets or sets the Updated At.
    /// </summary>
    [JsonProperty("updated_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdatedAt { get; set; }
}
