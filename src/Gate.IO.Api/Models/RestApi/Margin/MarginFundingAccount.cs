namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginFundingAccount
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }

    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    [JsonProperty("lent")]
    public decimal Lent { get; set; }

    [JsonProperty("total_lent")]
    public decimal TotalLent { get; set; }
}
