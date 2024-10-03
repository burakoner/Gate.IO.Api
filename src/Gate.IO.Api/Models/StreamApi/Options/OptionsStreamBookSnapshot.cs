using Gate.IO.Api.Models.RestApi.Options;

namespace Gate.IO.Api.Models.StreamApi.Options;

public  class OptionsStreamBookSnapshot
{
    [JsonProperty("t")]
    public DateTime Time { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("id")]
    public long OrderBookId { get; set; }

    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public List<OptionsOrderBookEntry> Bids { get; set; }=[];

    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public List<OptionsOrderBookEntry> Asks { get; set; }=[];
}
