namespace Gate.IO.Api.TradFi;

/// <summary>
/// Represents a TradFi candlestick stream update.
/// </summary>
public record GateTradFiStreamCandlestick
{
    /// <summary>
    /// Candlestick timestamp.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Subscription name in the format of interval_symbol.
    /// </summary>
    [JsonProperty("n")]
    public string Subscription { get; set; }

    /// <summary>
    /// Total volume.
    /// </summary>
    [JsonProperty("v")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Close price.
    /// </summary>
    [JsonProperty("c")]
    public decimal Close { get; set; }

    /// <summary>
    /// Highest price.
    /// </summary>
    [JsonProperty("h")]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price.
    /// </summary>
    [JsonProperty("l")]
    public decimal Low { get; set; }

    /// <summary>
    /// Open price.
    /// </summary>
    [JsonProperty("o")]
    public decimal Open { get; set; }

    /// <summary>
    /// Quote amount.
    /// </summary>
    [JsonProperty("a")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Whether the candlestick window is closed.
    /// </summary>
    [JsonProperty("w")]
    public bool IsClosed { get; set; }
}
