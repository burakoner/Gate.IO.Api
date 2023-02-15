namespace Gate.IO.Api.Models.RestApi.Margin;

public  class MarginAmount
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
