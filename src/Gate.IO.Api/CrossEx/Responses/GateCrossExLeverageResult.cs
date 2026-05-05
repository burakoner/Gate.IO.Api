namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx leverage update result
/// </summary>
public record GateCrossExLeverageResult
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
}
