namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P transaction detail
/// </summary>
public record GateP2pTransactionDetail
{
    /// <summary>
    /// Current user seller flag
    /// </summary>
    [JsonProperty("is_sell")]
    public int? IsSell { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("txid")]
    public long? TransactionId { get; set; }

    /// <summary>
    /// Advertisement order ID
    /// </summary>
    [JsonProperty("orderid")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Order creation timestamp
    /// </summary>
    [JsonProperty("timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Time { get; set; }

    /// <summary>
    /// Payment deadline
    /// </summary>
    [JsonProperty("last_pay_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LastPayTime { get; set; }

    /// <summary>
    /// Seconds left to pay
    /// </summary>
    [JsonProperty("remain_pay_time")]
    public long? RemainPayTime { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("currency_type")]
    public string CurrencyType { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("want_type")]
    public string WantType { get; set; }

    /// <summary>
    /// Fiat symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Crypto amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Fiat total
    /// </summary>
    [JsonProperty("total")]
    public decimal? Total { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Cancel reason ID
    /// </summary>
    [JsonProperty("reason_id")]
    public string ReasonId { get; set; }

    /// <summary>
    /// Cancel reason description
    /// </summary>
    [JsonProperty("reason_desc")]
    public string ReasonDescription { get; set; }

    /// <summary>
    /// Cancellation time text
    /// </summary>
    [JsonProperty("cancel_time")]
    public string CancelTime { get; set; }

    /// <summary>
    /// Dispute flag
    /// </summary>
    [JsonProperty("in_appeal")]
    public int? InAppeal { get; set; }

    /// <summary>
    /// Dispute timestamp
    /// </summary>
    [JsonProperty("dispute_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? DisputeTime { get; set; }

    /// <summary>
    /// Cancelable flag
    /// </summary>
    [JsonProperty("cancelable")]
    public int? Cancelable { get; set; }

    /// <summary>
    /// Hide payment flag
    /// </summary>
    [JsonProperty("hide_payment")]
    public int? HidePayment { get; set; }

    /// <summary>
    /// Trading tips
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    /// <summary>
    /// Bank display flag
    /// </summary>
    [JsonProperty("show_bank")]
    public string ShowBank { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    [JsonProperty("bankname")]
    public string BankName { get; set; }

    /// <summary>
    /// Bank branch
    /// </summary>
    [JsonProperty("bankbranch")]
    public string BankBranch { get; set; }

    /// <summary>
    /// Bank account or masked account
    /// </summary>
    [JsonProperty("bankid")]
    public string BankId { get; set; }

    /// <summary>
    /// Bank cardholder name
    /// </summary>
    [JsonProperty("bank_holder_realname")]
    public string BankHolderRealName { get; set; }

    /// <summary>
    /// Alipay display flag
    /// </summary>
    [JsonProperty("show_ali")]
    public string ShowAli { get; set; }

    /// <summary>
    /// Alipay account name
    /// </summary>
    [JsonProperty("aliname")]
    public string AliName { get; set; }

    /// <summary>
    /// Alipay QR flag
    /// </summary>
    [JsonProperty("is_alicode")]
    public int? IsAliCode { get; set; }

    /// <summary>
    /// WeChat display flag
    /// </summary>
    [JsonProperty("show_wechat")]
    public string ShowWechat { get; set; }

    /// <summary>
    /// WeChat account name
    /// </summary>
    [JsonProperty("wename")]
    public string WechatName { get; set; }

    /// <summary>
    /// Other payment display flag
    /// </summary>
    [JsonProperty("show_others")]
    public string ShowOthers { get; set; }

    /// <summary>
    /// Other payment methods
    /// </summary>
    [JsonProperty("pay_others")]
    public List<GateP2pPaymentOption> OtherPaymentOptions { get; set; } = [];

    /// <summary>
    /// Selected payment type
    /// </summary>
    [JsonProperty("sel_paytype")]
    public string SelectedPaymentType { get; set; }

    /// <summary>
    /// Counterparty encrypted UID
    /// </summary>
    [JsonProperty("its_uid")]
    public string CounterpartyUserId { get; set; }

    /// <summary>
    /// Counterparty nickname
    /// </summary>
    [JsonProperty("its_nickname")]
    public string CounterpartyNickname { get; set; }

    /// <summary>
    /// Counterparty real name
    /// </summary>
    [JsonProperty("its_realname")]
    public string CounterpartyRealName { get; set; }

    /// <summary>
    /// Previous trade flag
    /// </summary>
    [JsonProperty("have_traded")]
    public int? HaveTraded { get; set; }

    /// <summary>
    /// Appeal cancel permission
    /// </summary>
    [JsonProperty("appeal_allow_cancel")]
    public int? AppealAllowCancel { get; set; }

    /// <summary>
    /// Appeal verdict information
    /// </summary>
    [JsonProperty("appeal_verdict_has_open")]
    public string AppealVerdictHasOpen { get; set; }

    /// <summary>
    /// Unread chat count
    /// </summary>
    [JsonProperty("im_unread")]
    public int? UnreadMessages { get; set; }

    /// <summary>
    /// Payment voucher URLs
    /// </summary>
    [JsonProperty("payment_voucher_url")]
    public List<string> PaymentVoucherUrls { get; set; } = [];

    /// <summary>
    /// Timestamp when buyer confirmed payment
    /// </summary>
    [JsonProperty("timest_paid")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? PaidTime { get; set; }

    /// <summary>
    /// Current user's real name
    /// </summary>
    [JsonProperty("own_realname")]
    public string OwnRealName { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("order_type")]
    public int? OrderType { get; set; }

    /// <summary>
    /// Confirm receipt display flag
    /// </summary>
    [JsonProperty("is_show_receive")]
    public int? IsShowReceive { get; set; }

    /// <summary>
    /// Seller contact information display flag
    /// </summary>
    [JsonProperty("show_seller_contact_info")]
    public bool? ShowSellerContactInfo { get; set; }

    /// <summary>
    /// Supported payment types
    /// </summary>
    [JsonProperty("supported_pay_types")]
    public List<string> SupportedPaymentTypes { get; set; } = [];

    /// <summary>
    /// Gets or sets the Additional Data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
