namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Account
/// </summary>
public record GateSpotBalance
{
    /// <summary>
    /// Currency detail
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Available amount
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount, used in trading
    /// </summary>
    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    /// <summary>
    /// Version number
    /// </summary>
    [JsonProperty("update_id")]
    public long UpdateId { get; set; }
}
