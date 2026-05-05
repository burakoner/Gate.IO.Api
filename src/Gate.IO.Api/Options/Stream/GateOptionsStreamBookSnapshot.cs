namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Book Snapshot.
/// </summary>
public  class GateOptionsStreamBookSnapshot
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Order Book ID.
    /// </summary>
    [JsonProperty("id")]
    public long OrderBookId { get; set; }

    /// <summary>
    /// Gets or sets the Bids.
    /// </summary>
    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateOptionsOrderBookEntry> Bids { get; set; }=[];

    /// <summary>
    /// Gets or sets the Asks.
    /// </summary>
    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateOptionsOrderBookEntry> Asks { get; set; }=[];
}
