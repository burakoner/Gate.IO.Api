namespace Gate.IO.Api.Wallet;

/// <summary>
/// Sub Account Balance
/// </summary>
public record GateWalletSubAccountBalance
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

    /// <summary>
    /// Locked amount by currency
    /// </summary>
    [JsonProperty("locking")]
    public Dictionary<string, decimal> Locked { get; set; }
}
