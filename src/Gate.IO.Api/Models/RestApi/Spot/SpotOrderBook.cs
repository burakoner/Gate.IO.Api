namespace Gate.IO.Api.Models.RestApi.Spot;

/// <summary>
/// OrderBook
/// </summary>
public  class SpotOrderBook
{
    /// <summary>
    /// Order book ID, which is updated whenever the order book is changed. Valid only when &#x60;with_id&#x60; is set to &#x60;true&#x60;
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// The timestamp of the response data being generated (in milliseconds)
    /// </summary>
    [JsonProperty("current"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Current { get; set; }

    /// <summary>
    /// The timestamp of when the orderbook last changed (in milliseconds)
    /// </summary>
    [JsonProperty("update"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Update { get; set; }

    /// <summary>
    /// Asks order depth
    /// </summary>
    [JsonProperty("asks")]
    public IEnumerable<SpotOrderBookEntry> Asks { get; set; } = Array.Empty<SpotOrderBookEntry>();

    /// <summary>
    /// Bids order depth
    /// </summary>
    [JsonProperty("bids")]
    public IEnumerable<SpotOrderBookEntry> Bids { get; set; } = Array.Empty<SpotOrderBookEntry>();
}

[JsonConverter(typeof(ArrayConverter))]
public class SpotOrderBookEntry
{
    /// <summary>
    /// The price of this order book entry
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity of this price in the order book
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }
}
