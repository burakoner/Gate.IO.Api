namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures Candlestick
/// </summary>
public class GateFuturesCandlestickPremium
{
    /// <summary>
    /// Unix timestamp in seconds
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
}
