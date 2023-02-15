namespace Gate.IO.Api.Models.RestApi.Wallet;

/// <summary>
/// Spot Account
/// </summary>
public class SpotBalance
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
}
