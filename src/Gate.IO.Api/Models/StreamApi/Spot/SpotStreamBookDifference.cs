using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamBookDifference
{
    [JsonProperty("t")]
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
    public List<GateSpotOrderBookEntry> Bids { get; set; }=[];

    [JsonProperty("a", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateSpotOrderBookEntry> Asks { get; set; }=[];
}
