namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi candlestick
/// </summary>
public record GateTradFiCandlestick
{
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("o")]
    public decimal Open { get; set; }

    [JsonProperty("c")]
    public decimal Close { get; set; }

    [JsonProperty("h")]
    public decimal High { get; set; }

    [JsonProperty("l")]
    public decimal Low { get; set; }
}
