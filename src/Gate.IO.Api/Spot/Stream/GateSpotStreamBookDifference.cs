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

    //[JsonProperty("e")]
    //public string Event { get; set; }

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
