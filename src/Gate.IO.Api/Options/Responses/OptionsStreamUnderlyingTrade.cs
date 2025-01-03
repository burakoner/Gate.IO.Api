namespace Gate.IO.Api.Options;

public class OptionsStreamUnderlyingTrade
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("create_time_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("is_call")]
    public bool IsCall { get; set; }

}