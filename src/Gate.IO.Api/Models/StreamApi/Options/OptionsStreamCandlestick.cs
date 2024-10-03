namespace Gate.IO.Api.Models.StreamApi.Options;

public class OptionsStreamCandlestick
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

    [JsonProperty("a")]
    public decimal Amount { get; set; }

    [JsonProperty("n")]
    public string Subscription { get; set; }
}
