namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsCandlestick
{
    [JsonProperty("t")]
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
}
