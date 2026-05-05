namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order book price level.
/// </summary>
[JsonConverter(typeof(GateCrossExStreamOrderBookEntryConverter))]
public record GateCrossExStreamOrderBookEntry
{
    /// <summary>
    /// Price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public decimal Quantity { get; set; }
}
