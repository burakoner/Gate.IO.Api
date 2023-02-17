namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginAccountBalance
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }

    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}
