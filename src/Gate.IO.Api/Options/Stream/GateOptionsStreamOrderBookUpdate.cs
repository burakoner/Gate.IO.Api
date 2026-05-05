namespace Gate.IO.Api.Options;

/// <summary>
/// Represents a legacy Options order book price-level update.
/// </summary>
public record GateOptionsStreamOrderBookUpdate
{
    /// <summary>
    /// Options contract name.
    /// </summary>
    [JsonProperty("c")]
    public string Contract { get; set; }

    /// <summary>
    /// Order book price.
    /// </summary>
    [JsonProperty("p")]
    public decimal Price { get; set; }

    /// <summary>
    /// Final absolute size at this price. Positive values represent bids and negative values represent asks.
    /// </summary>
    [JsonProperty("s")]
    public long Size { get; set; }

    /// <summary>
    /// Price-level order book update ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
}
