namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures Stream Book Ticker.
/// </summary>
public  class GateFuturesStreamBookTicker
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
    [JsonProperty("b"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal BestBidPrice { get; set; }

    /// <summary>
    /// Gets or sets the Best Bid Amount.
    /// </summary>
    [JsonProperty("B")]
    public decimal BestBidAmount { get; set; }

    /// <summary>
    /// Gets or sets the Best Ask Price.
    /// </summary>
    [JsonProperty("a"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal BestAskPrice { get; set; }

    /// <summary>
    /// Gets or sets the Best Ask Amount.
    /// </summary>
    [JsonProperty("A")]
    public decimal BestAskAmount { get; set; }
}
