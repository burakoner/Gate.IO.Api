namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Transaction Id
/// </summary>
public record GateWalletTransactionId
{
    /// <summary>
    /// Transaction Id
    /// </summary>
    [JsonProperty("tx_id")]
    public string TransactionId { get; set; }
}
