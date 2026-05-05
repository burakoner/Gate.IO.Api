namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Book Difference.
/// </summary>
public record GateOptionsStreamBookDifference
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
    public List<GateOptionsOrderBookEntry> Bids { get; set; }=[];

    /// <summary>
    /// Gets or sets the Asks.
    /// </summary>
    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateOptionsOrderBookEntry> Asks { get; set; }=[];
}
