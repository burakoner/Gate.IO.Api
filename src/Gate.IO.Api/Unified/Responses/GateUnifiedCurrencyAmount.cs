namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Currency &amp; Amount Pair
/// </summary>
public record GateUnifiedCurrencyAmount
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; }

    /// <summary>
    /// Max borrowable
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
