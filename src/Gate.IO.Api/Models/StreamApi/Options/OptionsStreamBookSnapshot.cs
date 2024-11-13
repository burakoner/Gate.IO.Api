using Gate.IO.Api.Options;

namespace Gate.IO.Api.Models.StreamApi.Options;

public  class OptionsStreamBookSnapshot
{
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("id")]
    public long OrderBookId { get; set; }

    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateOptionsOrderBookEntry> Bids { get; set; }=[];

    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public List<GateOptionsOrderBookEntry> Asks { get; set; }=[];
}
