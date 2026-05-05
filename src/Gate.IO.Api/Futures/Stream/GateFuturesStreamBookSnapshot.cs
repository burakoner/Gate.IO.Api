namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures Stream Book Snapshot.
/// </summary>
public  class GateFuturesStreamBookSnapshot
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
    [JsonProperty("id")]
    public long LastUpdateId { get; set; }

    /// <summary>
    /// Gets or sets the depth level of the snapshot.
    /// </summary>
    [JsonProperty("l")]
    public int? Level { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("contract")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Bids.
    /// </summary>
    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateFuturesOrderBookEntry> Bids { get; set; }=[];

    /// <summary>
    /// Gets or sets the Asks.
    /// </summary>
    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateFuturesOrderBookEntry> Asks { get; set; }=[];
}
