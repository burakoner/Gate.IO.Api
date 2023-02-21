using Gate.IO.Api.Models.RestApi.Spot;

namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamBookSnapshot
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("lastUpdateId")]
    public long LastUpdateId { get; set; }

    [JsonProperty("s")]
    public string Symbol { get; set; }

    [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<SpotOrderBookEntry> Bids { get; set; }

    [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<SpotOrderBookEntry> Asks { get; set; }
}
