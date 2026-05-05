namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx leverage update result
/// </summary>
public record GateCrossExLeverageResult
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
}
