namespace Gate.IO.Api.Futures;

/// <summary>
/// OrderBook
/// </summary>
public record GateFuturesOrderBook
{
    /// <summary>
    /// Order book ID, which is updated whenever the order book is changed. Valid only when &#x60;with_id&#x60; is set to &#x60;true&#x60;
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// The timestamp of the response data being generated (in milliseconds)
    /// </summary>
    [JsonProperty("current")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Current { get; set; }

    /// <summary>
    /// The timestamp of when the orderbook last changed (in milliseconds)
    /// </summary>
    [JsonProperty("update")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Update { get; set; }

    /// <summary>
    /// Asks order depth
    /// </summary>
    [JsonProperty("asks")]
    public List<GateFuturesOrderBookEntry> Asks { get; set; } =[];

    /// <summary>
    /// Bids order depth
    /// </summary>
    [JsonProperty("bids")]
    public List<GateFuturesOrderBookEntry> Bids { get; set; } = [];
}

/// <summary>
/// GateFuturesOrderBookEntry
/// </summary>
public record GateFuturesOrderBookEntry
{
    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("p")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("s")]
    public decimal Size { get; set; }
}
