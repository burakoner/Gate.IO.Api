namespace Gate.IO.Api.Models.StreamApi.Futures;

public  class FuturesStreamBookSnapshot
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("id")]
    public long LastUpdateId { get; set; }

    [JsonProperty("contract")]
    public string Symbol { get; set; }

    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<FuturesOrderBookEntry> Bids { get; set; }

    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<FuturesOrderBookEntry> Asks { get; set; }
}
