namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Leverage
/// </summary>
public record GateMarginLeverage
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
}
