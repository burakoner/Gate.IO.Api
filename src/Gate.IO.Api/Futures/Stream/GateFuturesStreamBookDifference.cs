namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures Stream Book Difference.
/// </summary>
public  class GateFuturesStreamBookDifference
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets whether this update is a full depth snapshot.
    /// </summary>
    [JsonProperty("full")]
    public bool? IsFullSnapshot { get; set; }

    /// <summary>
    /// Gets or sets the depth level of the update.
    /// </summary>
    [JsonProperty("l")]
    public int? Level { get; set; }

    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("s")]
    public string Contract { get; set; }

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
    public List<GateFuturesOrderBookEntry> Bids { get; set; }=[];

    /// <summary>
    /// Gets or sets the Asks.
    /// </summary>
    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateFuturesOrderBookEntry> Asks { get; set; }=[];
}
