namespace Gate.IO.Api.Options;

public class OptionsStreamUnderlyingPrice
{
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("time")]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}