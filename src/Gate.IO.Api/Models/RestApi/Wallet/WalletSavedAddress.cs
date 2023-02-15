namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletSavedAddress
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("tag")]
    public string Tag { get; set; }
    
    [JsonProperty("verified")]
    public bool Verified{ get; set; }
}
