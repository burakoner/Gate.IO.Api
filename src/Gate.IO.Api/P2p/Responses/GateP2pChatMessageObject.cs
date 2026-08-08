namespace Gate.IO.Api.P2p;

/// <summary>
/// Structured P2P chat message payload
/// </summary>
public record GateP2pChatMessageObject
{
    /// <summary>
    /// Order status when the message was sent
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Status-message text
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Payment vouchers
    /// </summary>
    [JsonProperty("payment_voucher")]
    public List<JToken> PaymentVouchers { get; set; }

    /// <summary>
    /// Cancellation reason ID
    /// </summary>
    [JsonProperty("reason_id")]
    public int? ReasonId { get; set; }

    /// <summary>
    /// Cancellation-reason popup ID
    /// </summary>
    [JsonProperty("toast_id")]
    public int? ToastId { get; set; }

    /// <summary>
    /// Cancellation reason description
    /// </summary>
    [JsonProperty("reason_memo")]
    public string ReasonMemo { get; set; }

    /// <summary>
    /// Cancellation time
    /// </summary>
    [JsonProperty("cancel_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CancelTime { get; set; }

    /// <summary>
    /// Seller cancellation confirmation: 0 pending, 1 confirmed, 2 rejected
    /// </summary>
    [JsonProperty("seller_confirm")]
    public int? SellerConfirmation { get; set; }

    /// <summary>
    /// Payment method ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Payment method description
    /// </summary>
    [JsonProperty("account_des")]
    public string AccountDescription { get; set; }

    /// <summary>
    /// Payment method type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// Payment method file link
    /// </summary>
    [JsonProperty("file")]
    public string File { get; set; }

    /// <summary>
    /// Payment method file key
    /// </summary>
    [JsonProperty("file_key")]
    public string FileKey { get; set; }

    /// <summary>
    /// Payment account or masked account
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// Payment method note
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Payment method code
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Additional payment method note
    /// </summary>
    [JsonProperty("memo_ext")]
    public string MemoExtension { get; set; }

    /// <summary>
    /// Payment method tip
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    /// <summary>
    /// Payment method account holder name
    /// </summary>
    [JsonProperty("real_name")]
    public string RealName { get; set; }

    /// <summary>
    /// Whether the payment method was deleted
    /// </summary>
    [JsonProperty("is_delete")]
    public int? IsDeleted { get; set; }

    /// <summary>
    /// Full payment method name
    /// </summary>
    [JsonProperty("pay_name")]
    public string PayName { get; set; }
}
