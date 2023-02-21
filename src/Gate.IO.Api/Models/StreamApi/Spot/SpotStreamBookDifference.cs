using Gate.IO.Api.Models.RestApi.Spot;

namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamBookDifference
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    //[JsonProperty("e")]
    //public string Event { get; set; }

    [JsonProperty("s")]
    public string Symbol { get; set; }

    [JsonProperty("U")]
    public long OrderBookFirstUpdateId { get; set; }
    
    [JsonProperty("u")]
    public long OrderBookLastUpdateId { get; set; }

    [JsonProperty("b", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<SpotOrderBookEntry> Bids { get; set; }

    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<SpotOrderBookEntry> Asks { get; set; }
}
