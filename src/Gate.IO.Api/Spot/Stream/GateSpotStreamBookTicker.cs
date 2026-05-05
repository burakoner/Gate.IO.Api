namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Book Ticker.
/// </summary>
public  class GateSpotStreamBookTicker
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Order Book Update ID.
    /// </summary>
    [JsonProperty("u")]
    public long OrderBookUpdateId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Best Bid Price.
    /// </summary>
    [JsonProperty("b")]
    public decimal BestBidPrice { get; set; }

    /// <summary>
    /// Gets or sets the Best Bid Amount.
    /// </summary>
    [JsonProperty("B")]
    public decimal BestBidAmount { get; set; }

    /// <summary>
    /// Gets or sets the Best Ask Price.
    /// </summary>
    [JsonProperty("a")]
    public decimal BestAskPrice { get; set; }

    /// <summary>
    /// Gets or sets the Best Ask Amount.
    /// </summary>
    [JsonProperty("A")]
    public decimal BestAskAmount { get; set; }
}
