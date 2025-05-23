namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Push
/// </summary>
public record GateWalletTransfer
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Initiator User ID
    /// </summary>
    [JsonProperty("push_uid")]
    public long InitiatorUserId { get; set; }

    /// <summary>
    /// Recipient User ID
    /// </summary>
    [JsonProperty("receive_uid")]
    public long RecipientUserId { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Transfer Status
    /// </summary>
    [JsonProperty("status")]
    public GateWalletUidTransferState Status { get; set; }

    /// <summary>
    /// PENDING Reason Tips
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Order Type
    /// </summary>
    [JsonProperty("transaction_type")]
    public GateWalletTransferType TransferType { get; set; }
}
