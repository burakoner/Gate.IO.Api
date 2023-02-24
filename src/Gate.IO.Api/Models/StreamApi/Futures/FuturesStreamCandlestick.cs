namespace Gate.IO.Api.Models.StreamApi.Futures;

public  class FuturesStreamCandlestick
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("o")]
    public decimal Open { get; set; }

    [JsonProperty("h")]
    public decimal High { get; set; }

    [JsonProperty("l")]
    public decimal Low { get; set; }

    [JsonProperty("c")]
    public decimal Close { get; set; }

    [JsonProperty("v")]
    public decimal Volume { get; set; }

    [JsonProperty("n")]
    public string Subscription { get; set; }
}
