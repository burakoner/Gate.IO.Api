namespace Gate.IO.Api.Futures;

/// <summary>
/// FuturesFundingRate
/// </summary>
public class GateFuturesFundingRate
{
    /// <summary>
    /// Unix timestamp in seconds
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Funding rate
    /// </summary>
    [JsonProperty("r")]
    public decimal Rate { get; set; }
}
