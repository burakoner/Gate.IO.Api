namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Transaction
/// </summary>
public class GateWalletTransaction
{
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Hash record of the withdrawal
    /// </summary>
    [JsonProperty("txid")]
    public string TransactionId { get; set; }

    /// <summary>
    /// Client order id, up to 32 length and can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("withdraw_order_id")]
    public string WithdrawalOrderId { get; set; }

    /// <summary>
    /// Operation time
    /// </summary>
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Currency amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Withdrawal address. Required for withdrawals
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Additional remarks with regards to the withdrawal
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Record status.
    /// </summary>
    [JsonProperty("status")]
    public GateWalletWithdrawalStatus Status { get; set; }

    /// <summary>
    /// Name of the chain used in withdrawals
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }
}
