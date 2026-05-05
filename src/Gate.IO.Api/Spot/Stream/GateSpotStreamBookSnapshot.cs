namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Book Snapshot.
/// </summary>
public  class GateSpotStreamBookSnapshot
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Last Update ID.
    /// </summary>
    [JsonProperty("lastUpdateId")]
    public long LastUpdateId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the depth level.
    /// </summary>
    [JsonProperty("l")]
    public string Level { get; set; }

    /// <summary>
    /// Gets or sets the Bids.
    /// </summary>
    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Bids { get; set; }=[];

    /// <summary>
    /// Gets or sets the Asks.
    /// </summary>
    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Asks { get; set; }=[];
}
