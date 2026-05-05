namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx full limited-level order book stream update.
/// </summary>
public record GateCrossExStreamOrderBook
{
    /// <summary>
    /// Exchange timestamp in milliseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Ask depth.
    /// </summary>
    [JsonProperty("a")]
    public List<GateCrossExStreamOrderBookEntry> Asks { get; set; } = [];

    /// <summary>
    /// Bid depth.
    /// </summary>
    [JsonProperty("b")]
    public List<GateCrossExStreamOrderBookEntry> Bids { get; set; } = [];
}
