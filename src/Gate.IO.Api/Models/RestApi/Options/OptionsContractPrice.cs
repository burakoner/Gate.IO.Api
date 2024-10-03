namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsContractPrice
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("time")]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}