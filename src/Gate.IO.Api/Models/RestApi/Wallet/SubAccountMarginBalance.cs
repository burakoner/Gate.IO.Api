namespace Gate.IO.Api.Models.RestApi.Wallet;

public class SubAccountMarginBalance
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("available")]
    public Dictionary<string, decimal> Available { get; set; }
}

public class SubAccountMarginBalanceAvailable
{
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    [JsonProperty("base")]
    public SubAccountMarginBalanceAvailableItem Base { get; set; }

    [JsonProperty("quote")]
    public SubAccountMarginBalanceAvailableItem Quote { get; set; }
}

public class SubAccountMarginBalanceAvailableItem
{
    [JsonProperty("available")]
    public decimal Available { get; set; }
    
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }
    
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
    
    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    [JsonProperty("locked")]
    public bool Locked { get; set; }
}
