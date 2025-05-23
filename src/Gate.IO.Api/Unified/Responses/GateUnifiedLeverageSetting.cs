namespace Gate.IO.Api.Unified;

/// <summary>
/// Leverage setting
/// </summary>
public record GateUnifiedLeverageSetting
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
}
