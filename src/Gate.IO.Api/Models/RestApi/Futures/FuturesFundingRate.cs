namespace Gate.IO.Api.Models.RestApi.Futures;

public  class FuturesFundingRate
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("r")]
    public decimal Rate { get; set; }
}
