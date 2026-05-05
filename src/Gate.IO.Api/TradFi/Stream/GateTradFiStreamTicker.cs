namespace Gate.IO.Api.TradFi;

/// <summary>
/// Represents a TradFi ticker stream update.
/// </summary>
public record GateTradFiStreamTicker
{
    /// <summary>
    /// Quote timestamp.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// TradFi symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Opening price.
    /// </summary>
    [JsonProperty("open_price")]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// Last price.
    /// </summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Price change amount.
    /// </summary>
    [JsonProperty("price_change_amount")]
    public decimal PriceChangeAmount { get; set; }

    /// <summary>
    /// Price change percentage.
    /// </summary>
    [JsonProperty("price_change_rate")]
    public decimal PriceChangeRate { get; set; }

    /// <summary>
    /// Highest price of the day.
    /// </summary>
    [JsonProperty("high")]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price of the day.
    /// </summary>
    [JsonProperty("low")]
    public decimal Low { get; set; }
}
