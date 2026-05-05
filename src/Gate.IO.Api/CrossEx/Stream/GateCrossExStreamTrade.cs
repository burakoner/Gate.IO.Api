namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx public trade stream update.
/// </summary>
public record GateCrossExStreamTrade
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Exchange raw trade ID.
    /// </summary>
    [JsonProperty("i")]
    public string TradeId { get; set; }

    /// <summary>
    /// Trade price.
    /// </summary>
    [JsonProperty("p")]
    public decimal Price { get; set; }

    /// <summary>
    /// Trade quantity.
    /// </summary>
    [JsonProperty("q")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Trade side.
    /// </summary>
    [JsonProperty("S")]
    public string Side { get; set; }

    /// <summary>
    /// Exchange timestamp in milliseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Whether the buyer is maker, when provided by the exchange.
    /// </summary>
    [JsonProperty("m")]
    public bool? IsBuyerMaker { get; set; }
}
