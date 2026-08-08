namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet deposit record
/// </summary>
public record GateWalletDeposit
{
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string TransactionId { get; set; }

    /// <summary>
    /// Operation time
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Token amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Additional deposit remark
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Deposit status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateWalletDepositStatus Status { get; set; }

    /// <summary>
    /// Blocked deposit refund status. Returned only for a blocked deposit refund record with a non-empty status.
    /// </summary>
    [JsonProperty("refund_status"), JsonConverter(typeof(MapConverter))]
    public GateWalletDepositRefundStatus? RefundStatus { get; set; }

    /// <summary>
    /// Chain name
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }
}
