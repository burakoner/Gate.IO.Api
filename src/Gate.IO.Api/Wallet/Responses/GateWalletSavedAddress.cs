namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Saved Address
/// </summary>
public class GateWalletSavedAddress
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Chain name
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; }

    /// <summary>
    /// Whether to pass the verification 0-unverified, 1-verified
    /// </summary>
    [JsonProperty("verified")]
    public bool Verified { get; set; }
}
