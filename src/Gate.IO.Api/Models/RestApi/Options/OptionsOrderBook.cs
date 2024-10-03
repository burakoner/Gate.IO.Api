namespace Gate.IO.Api.Models.RestApi.Options;

public  class OptionsOrderBook
{
    [JsonProperty("id")]
    public long? Id { get; set; }

    [JsonProperty("current")]
    public DateTime Current { get; set; }

    [JsonProperty("update")]
    public DateTime Update { get; set; }

    [JsonProperty("asks")]
    public List<OptionsOrderBookEntry> Asks { get; set; } =[];

    [JsonProperty("bids")]
    public List<OptionsOrderBookEntry> Bids { get; set; } =[];
}

public class OptionsOrderBookEntry
{
    [JsonProperty("p")]
    public decimal Price { get; set; }

    [JsonProperty("s")]
    public decimal Size { get; set; }
}
