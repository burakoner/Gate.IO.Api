using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Book Difference.
/// </summary>
public  class GateSpotStreamBookDifference
{
    /// <summary>
    /// Gets or sets the Time.
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
    /// Depth level.
    /// </summary>
    [JsonProperty("l")]
    public string Level { get; set; }

    /// <summary>
    /// Event name returned by Gate. This field is documented as ignorable.
    /// </summary>
    [JsonProperty("e")]
    public string EventName { get; set; }

    /// <summary>
    /// Deprecated update timestamp in seconds.
    /// </summary>
    [JsonProperty("E")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? EventTime { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Order Book First Update ID.
    /// </summary>
    [JsonProperty("U")]
    public long OrderBookFirstUpdateId { get; set; }
    
    /// <summary>
    /// Gets or sets the Order Book Last Update ID.
    /// </summary>
    [JsonProperty("u")]
    public long OrderBookLastUpdateId { get; set; }

    /// <summary>
    /// Gets or sets the Bids.
    /// </summary>
    [JsonProperty("b", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Bids { get; set; }=[];

    /// <summary>
    /// Gets or sets the Asks.
    /// </summary>
    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Asks { get; set; }=[];
}
