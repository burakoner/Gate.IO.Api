namespace Gate.IO.Api.Delivery;

/// <summary>
/// Represents a legacy Delivery order book price-level update.
/// </summary>
public record GateDeliveryStreamOrderBookUpdate
{
    /// <summary>
    /// Delivery contract name.
    /// </summary>
    [JsonProperty("contract")]
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
    public decimal Size { get; set; }

    /// <summary>
    /// Price-level order book update ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
}
