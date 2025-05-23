namespace Gate.IO.Api.Wallet;

/// <summary>
/// Sub Account Futures Balance
/// </summary>
public record GateWalletSubAccountFuturesBalance
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Futures account balances
    /// </summary>
    [JsonProperty("available")]
    public Dictionary<string, GateFuturesBalance> Available { get; set; }
}