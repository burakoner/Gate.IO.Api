namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha account asset.
/// </summary>
public record GateAlphaAccount
{
    /// <summary>
    /// Currency name.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Available balance.
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked balance.
    /// </summary>
    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    /// <summary>
    /// Token contract address.
    /// </summary>
    [JsonProperty("token_address")]
    public string TokenAddress { get; set; }

    /// <summary>
    /// Blockchain name.
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }
}
