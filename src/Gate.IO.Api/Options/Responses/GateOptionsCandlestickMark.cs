namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsCandlestickMark
/// </summary>
public class GateOptionsCandlestickMark
{
    /// <summary>
    /// Unix timestamp in seconds
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Size volume (contract size). Only returned if contract is not prefixed
    /// </summary>
    [JsonProperty("v")]
    public decimal? Volume { get; set; }

    /// <summary>
    /// Close price (quote currency, unit: underlying corresponding option price)
    /// </summary>
    [JsonProperty("c")]
    public decimal Close { get; set; }

    /// <summary>
    /// Highest price (quote currency, unit: underlying corresponding option price)
    /// </summary>
    [JsonProperty("h")]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price (quote currency, unit: underlying corresponding option price)
    /// </summary>
    [JsonProperty("l")]
    public decimal Low { get; set; }

    /// <summary>
    /// Open price (quote currency, unit: underlying corresponding option price)
    /// </summary>
    [JsonProperty("o")]
    public decimal Open { get; set; }

    /// <summary>
    /// Trading volume (unit: Quote currency)
    /// </summary>
    [JsonProperty("sum")]
    public decimal QuoteVolume { get; set; }
}
