namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest currency configuration
/// </summary>
public record GateEarnAutoInvestConfig
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Investment limit
    /// </summary>
    [JsonProperty("max_limit")]
    public decimal MaxLimit { get; set; }
}
