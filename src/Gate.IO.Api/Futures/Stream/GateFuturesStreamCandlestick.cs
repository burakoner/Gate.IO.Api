namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Futures Stream Candlestick.
/// </summary>
public  class FuturesStreamCandlestick
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Open.
    /// </summary>
    [JsonProperty("o")]
    public decimal Open { get; set; }

    /// <summary>
    /// Gets or sets the High.
    /// </summary>
    [JsonProperty("h")]
    public decimal High { get; set; }

    /// <summary>
    /// Gets or sets the Low.
    /// </summary>
    [JsonProperty("l")]
    public decimal Low { get; set; }

    /// <summary>
    /// Gets or sets the Close.
    /// </summary>
    [JsonProperty("c")]
    public decimal Close { get; set; }

    /// <summary>
    /// Gets or sets the Volume.
    /// </summary>
    [JsonProperty("v")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Gets or sets the Subscription.
    /// </summary>
    [JsonProperty("n")]
    public string Subscription { get; set; }
}
