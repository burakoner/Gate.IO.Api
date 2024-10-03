namespace Gate.IO.Api.Futures;

public class FuturesInsuranceBalance
{
    [JsonProperty("t"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("b")]
    public decimal Balance { get; set; }
}
