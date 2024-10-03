namespace Gate.IO.Api.Models.StreamApi.Futures;

public  class FuturesStreamBookDifference
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    //[JsonProperty("e")]
    //public string Event { get; set; }

    [JsonProperty("s")]
    public string Contract { get; set; }

    [JsonProperty("U")]
    public long OrderBookFirstUpdateId { get; set; }
    
    [JsonProperty("u")]
    public long OrderBookLastUpdateId { get; set; }

    [JsonProperty("b", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<FuturesOrderBookEntry> Bids { get; set; }

    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<FuturesOrderBookEntry> Asks { get; set; }
}
