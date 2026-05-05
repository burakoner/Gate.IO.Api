namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot order book V2 update notification.
/// </summary>
public class GateSpotStreamOrderBookV2Update
{
    /// <summary>
    /// Order book generation timestamp in milliseconds.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Whether this update is a full depth snapshot.
    /// </summary>
    [JsonProperty("full")]
    public bool? IsFullSnapshot { get; set; }

    /// <summary>
    /// Depth stream name, for example ob.BTC_USDT.50.
    /// </summary>
    [JsonProperty("s")]
    public string Stream { get; set; }

    /// <summary>
    /// Starting order book update ID of this update.
    /// </summary>
    [JsonProperty("U")]
    public long? FirstUpdateId { get; set; }

    /// <summary>
    /// Ending order book update ID of this update.
    /// </summary>
    [JsonProperty("u")]
    public long LastUpdateId { get; set; }

    /// <summary>
    /// Bid updates since the previous update.
    /// </summary>
    [JsonProperty("b", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Bids { get; set; } = [];

    /// <summary>
    /// Ask updates since the previous update.
    /// </summary>
    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Asks { get; set; } = [];
}
