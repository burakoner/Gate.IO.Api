namespace Gate.IO.Api.Models.RestApi.Wallet;

public class MarginBalance
{
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("locked")]
    public bool Locked { get; set; }

    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    [JsonProperty("base")]
    public MarginAssetBalance Base { get; set; }

    [JsonProperty("quote")]
    public MarginAssetBalance Quote { get; set; }
}

public class MarginAssetBalance
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
