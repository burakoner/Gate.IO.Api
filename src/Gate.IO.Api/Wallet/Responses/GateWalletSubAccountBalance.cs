namespace Gate.IO.Api.Wallet;

/// <summary>
/// Sub Account Balance
/// </summary>
public class GateWalletSubAccountBalance
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Available balances of currencies
    /// </summary>
    [JsonProperty("available")]
    public Dictionary<string, decimal> Available { get; set; }
}
