namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesAccountBook
{
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(FuturesAccountBookType))]
    public FuturesAccountBookType Type { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("text")]
    public string Comment { get; set; }
}
