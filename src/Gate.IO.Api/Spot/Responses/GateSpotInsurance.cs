namespace Gate.IO.Api.Spot;

/// <summary>
/// Gate Spot Insurance History Record
/// </summary>
public record GateSpotInsurance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("balance")]
    public long Balance { get; set; }

    /// <summary>
    /// Creation time, timestamp, milliseconds
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
