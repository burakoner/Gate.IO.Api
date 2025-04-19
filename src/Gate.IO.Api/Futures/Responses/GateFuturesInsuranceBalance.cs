namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesInsuranceBalance
/// </summary>
public record GateFuturesInsuranceBalance
{
    /// <summary>
    /// Unix timestamp in seconds
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Insurance balance
    /// </summary>
    [JsonProperty("b")]
    public decimal Balance { get; set; }
}
