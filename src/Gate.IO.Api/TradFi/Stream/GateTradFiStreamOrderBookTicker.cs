namespace Gate.IO.Api.TradFi;

/// <summary>
/// Represents a TradFi best bid and ask stream update.
/// </summary>
public record GateTradFiStreamOrderBookTicker
{
    /// <summary>
    /// Quote timestamp.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// TradFi symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Best bid price.
    /// </summary>
    [JsonProperty("bid")]
    public decimal Bid { get; set; }

    /// <summary>
    /// Best ask price.
    /// </summary>
    [JsonProperty("ask")]
    public decimal Ask { get; set; }
}
