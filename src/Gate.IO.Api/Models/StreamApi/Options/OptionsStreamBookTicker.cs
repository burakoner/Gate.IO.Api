namespace Gate.IO.Api.Models.StreamApi.Options;

public  class OptionsStreamBookTicker
{
    [JsonProperty("t")]
    public DateTime Time { get; set; }

    [JsonProperty("u")]
    public long OrderBookUpdateId { get; set; }

    [JsonProperty("s")]
    public string Symbol { get; set; }

    [JsonProperty("b")]
    public decimal BestBidPrice { get; set; }

    [JsonProperty("B")]
    public decimal BestBidAmount { get; set; }

    [JsonProperty("a")]
    public decimal BestAskPrice { get; set; }

    [JsonProperty("A")]
    public decimal BestAskAmount { get; set; }
}
