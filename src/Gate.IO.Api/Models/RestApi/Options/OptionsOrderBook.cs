namespace Gate.IO.Api.Models.RestApi.Options;

public  class OptionsOrderBook
{
    [JsonProperty("id")]
    public long? Id { get; set; }

    [JsonProperty("current"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Current { get; set; }

    [JsonProperty("update"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Update { get; set; }

    [JsonProperty("asks")]
    public IEnumerable<OptionsOrderBookEntry> Asks { get; set; } = Array.Empty<OptionsOrderBookEntry>();

    [JsonProperty("bids")]
    public IEnumerable<OptionsOrderBookEntry> Bids { get; set; } = Array.Empty<OptionsOrderBookEntry>();
}

public class OptionsOrderBookEntry
{
    [JsonProperty("p")]
    public decimal Price { get; set; }

    [JsonProperty("s")]
    public decimal Size { get; set; }
}
