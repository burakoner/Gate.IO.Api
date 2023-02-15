namespace Gate.IO.Api.Models.RestApi.Spot;

[JsonConverter(typeof(ArrayConverter))]
public  class SpotCandlestick
{
    /// <summary>
    /// Unix timestamp in seconds
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Quote currency trading volume
    /// </summary>
    [ArrayProperty(1)]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Close price (quote currency)
    /// </summary>
    [ArrayProperty(2)]
    public decimal Close { get; set; }

    /// <summary>
    /// Highest price (quote currency)
    /// </summary>
    [ArrayProperty(3)]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price (quote currency)
    /// </summary>
    [ArrayProperty(4)]
    public decimal Low { get; set; }

    /// <summary>
    /// Open price (quote currency)
    /// </summary>
    [ArrayProperty(5)]
    public decimal Open { get; set; }

    /// <summary>
    /// Base currency trading amount
    /// </summary>
    [ArrayProperty(6)]
    public decimal Volume { get; set; }
}
