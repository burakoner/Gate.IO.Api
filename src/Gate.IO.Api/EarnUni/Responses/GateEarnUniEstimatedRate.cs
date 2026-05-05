namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni estimated annualized interest rate
/// </summary>
public record GateEarnUniEstimatedRate
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Estimated annualized rate
    /// </summary>
    [JsonProperty("est_rate")]
    public decimal EstimatedRate { get; set; }
}
