namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsTrade
{
    [JsonProperty("id")]
    public long Id{ get; set; }

    [JsonProperty("create_time")]
    public DateTime Time { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
}