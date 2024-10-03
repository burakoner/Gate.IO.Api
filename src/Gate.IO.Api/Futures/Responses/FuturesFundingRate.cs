namespace Gate.IO.Api.Futures;

public class FuturesFundingRate
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("r")]
    public decimal Rate { get; set; }
}
