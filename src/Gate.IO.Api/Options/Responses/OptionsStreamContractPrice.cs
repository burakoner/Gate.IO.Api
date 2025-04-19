namespace Gate.IO.Api.Options;

public record OptionsStreamContractPrice
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}