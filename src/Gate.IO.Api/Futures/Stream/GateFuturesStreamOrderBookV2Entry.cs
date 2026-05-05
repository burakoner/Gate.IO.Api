namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents a Futures order book V2 price level.
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record GateFuturesStreamOrderBookV2Entry
{
    /// <summary>
    /// Price of the order book level.
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    /// <summary>
    /// Size available at this price level.
    /// </summary>
    [ArrayProperty(1)]
    public decimal Size { get; set; }
}
