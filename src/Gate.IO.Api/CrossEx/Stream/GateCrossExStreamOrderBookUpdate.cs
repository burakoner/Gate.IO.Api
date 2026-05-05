namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx incremental order book stream update.
/// </summary>
public record GateCrossExStreamOrderBookUpdate : GateCrossExStreamOrderBook
{
    /// <summary>
    /// Whether the message is a snapshot.
    /// </summary>
    [JsonProperty("snapshot")]
    public bool IsSnapshot { get; set; }

    /// <summary>
    /// First update ID.
    /// </summary>
    [JsonProperty("U")]
    public long FirstUpdateId { get; set; }

    /// <summary>
    /// Last update ID.
    /// </summary>
    [JsonProperty("u")]
    public long LastUpdateId { get; set; }
}
