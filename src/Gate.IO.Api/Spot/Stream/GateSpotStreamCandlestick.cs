namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Candlestick.
/// </summary>
public  class GateSpotStreamCandlestick
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Close price (quote currency)
    /// </summary>
    [JsonProperty("c")]
    public decimal Close { get; set; }

    /// <summary>
    /// Highest price (quote currency)
    /// </summary>
    [JsonProperty("h")]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price (quote currency)
    /// </summary>
    [JsonProperty("l")]
    public decimal Low { get; set; }

    /// <summary>
    /// Open price (quote currency)
    /// </summary>
    [JsonProperty("o")]
    public decimal Open { get; set; }

    /// <summary>
    /// Total volume
    /// </summary>
    [JsonProperty("v")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Base currency trading amount
    /// </summary>
    [JsonProperty("a")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// Name of the subscription, in the format interval_currency-pair
    /// </summary>
    [JsonProperty("n")]
    public string Subscription { get; set; }

    /// <summary>
    /// Whether this candlestick window is closed.
    /// </summary>
    [JsonProperty("w")]
    public bool? IsClosed { get; set; }
}
