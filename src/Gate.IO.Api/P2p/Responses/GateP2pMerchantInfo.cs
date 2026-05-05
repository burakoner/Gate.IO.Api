namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P merchant market information
/// </summary>
public record GateP2pMerchantInfo
{
    /// <summary>
    /// Merchant type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Market
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }
}
