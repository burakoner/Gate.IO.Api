namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginAccount
{
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("locked")]
    public bool Locked { get; set; }

    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    [JsonProperty("base")]
    public MarginAccountBalance Base { get; set; }

    [JsonProperty("quote")]
    public MarginAccountBalance Quote { get; set; }
}