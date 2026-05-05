namespace Gate.IO.Api.P2p;

internal record GateP2pResponse<T> where T : class
{
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}

/// <summary>
/// P2P action result
/// </summary>
public record GateP2pActionResult
{
    /// <summary>
    /// Response timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Placeholder method returned by Gate
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; }

    /// <summary>
    /// Return code
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// API version
    /// </summary>
    [JsonProperty("version")]
    public string Version { get; set; }
}

/// <summary>
/// P2P merchant or counterparty user information
/// </summary>
public record GateP2pUserInfo
{
    /// <summary>
    /// Whether this is the authenticated user
    /// </summary>
    [JsonProperty("is_self")]
    public bool? IsSelf { get; set; }

    /// <summary>
    /// User registration time text
    /// </summary>
    [JsonProperty("user_timest")]
    public string UserTime { get; set; }

    /// <summary>
    /// Number of counterparties
    /// </summary>
    [JsonProperty("counterparties_num")]
    public int? CounterpartiesNumber { get; set; }

    /// <summary>
    /// Email verification flag
    /// </summary>
    [JsonProperty("email_verified")]
    public string EmailVerified { get; set; }

    /// <summary>
    /// KYC verification flag
    /// </summary>
    [JsonProperty("verified")]
    public string Verified { get; set; }

    /// <summary>
    /// Phone binding flag
    /// </summary>
    [JsonProperty("has_phone")]
    public string HasPhone { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    [JsonProperty("user_name")]
    public string UserName { get; set; }

    /// <summary>
    /// User note
    /// </summary>
    [JsonProperty("user_note")]
    public string UserNote { get; set; }

    /// <summary>
    /// Total completed orders
    /// </summary>
    [JsonProperty("complete_transactions")]
    public long? CompleteTransactions { get; set; }

    /// <summary>
    /// Completed buy orders
    /// </summary>
    [JsonProperty("paid_transactions")]
    public long? PaidTransactions { get; set; }

    /// <summary>
    /// Completed sell orders
    /// </summary>
    [JsonProperty("accepted_transactions")]
    public long? AcceptedTransactions { get; set; }

    /// <summary>
    /// Average receipt confirmation time
    /// </summary>
    [JsonProperty("transactions_used_time")]
    public long? TransactionsUsedTime { get; set; }

    /// <summary>
    /// Cancellation count in the last 30 days
    /// </summary>
    [JsonProperty("cancelled_used_time_month")]
    public long? CancelledUsedTimeMonth { get; set; }

    /// <summary>
    /// Completed orders in the last 30 days
    /// </summary>
    [JsonProperty("complete_transactions_month")]
    public long? CompleteTransactionsMonth { get; set; }

    /// <summary>
    /// Completion rate in the last 30 days
    /// </summary>
    [JsonProperty("complete_rate_month")]
    public decimal? CompleteRateMonth { get; set; }

    /// <summary>
    /// Buy order ratio in the last 30 days
    /// </summary>
    [JsonProperty("orders_buy_rate_month")]
    public decimal? OrdersBuyRateMonth { get; set; }

    /// <summary>
    /// Block flag
    /// </summary>
    [JsonProperty("is_black")]
    public int? IsBlack { get; set; }

    /// <summary>
    /// Follow flag
    /// </summary>
    [JsonProperty("is_follow")]
    public int? IsFollow { get; set; }

    /// <summary>
    /// Previous trade flag
    /// </summary>
    [JsonProperty("have_traded")]
    public int? HaveTraded { get; set; }

    /// <summary>
    /// Encrypted user ID
    /// </summary>
    [JsonProperty("biz_uid")]
    public string BusinessUserId { get; set; }

    /// <summary>
    /// Blue VIP flag
    /// </summary>
    [JsonProperty("blue_vip")]
    public int? BlueVip { get; set; }

    /// <summary>
    /// Merchant work status
    /// </summary>
    [JsonProperty("work_status")]
    public int? WorkStatus { get; set; }

    /// <summary>
    /// Account age in days
    /// </summary>
    [JsonProperty("registration_days")]
    public int? RegistrationDays { get; set; }

    /// <summary>
    /// Days since first trade
    /// </summary>
    [JsonProperty("first_trade_days")]
    public int? FirstTradeDays { get; set; }

    /// <summary>
    /// Additional margin flag
    /// </summary>
    [JsonProperty("need_replenish")]
    public int? NeedReplenish { get; set; }

    /// <summary>
    /// Merchant market information
    /// </summary>
    [JsonProperty("merchant_info")]
    public GateP2pMerchantInfo MerchantInfo { get; set; }

    /// <summary>
    /// Merchant online status
    /// </summary>
    [JsonProperty("online_status")]
    public int? OnlineStatus { get; set; }

    /// <summary>
    /// Merchant work hours
    /// </summary>
    [JsonProperty("work_hours")]
    public JToken WorkHours { get; set; }

    /// <summary>
    /// 30-day transaction volume
    /// </summary>
    [JsonProperty("transactions_month")]
    public decimal? TransactionsMonth { get; set; }

    /// <summary>
    /// Total transaction volume
    /// </summary>
    [JsonProperty("transactions_all")]
    public decimal? TransactionsAll { get; set; }

    /// <summary>
    /// Composite user flag
    /// </summary>
    [JsonProperty("trade_versatile")]
    public bool? TradeVersatile { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

/// <summary>
/// P2P merchant market information
/// </summary>
public record GateP2pMerchantInfo
{
    /// <summary>
    /// Merchant type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Market
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }
}

/// <summary>
/// P2P payment method group
/// </summary>
public record GateP2pPaymentMethodGroup
{
    /// <summary>
    /// Payment type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// Payment display name
    /// </summary>
    [JsonProperty("pay_name")]
    public string PayName { get; set; }

    /// <summary>
    /// Bound payment method IDs
    /// </summary>
    [JsonProperty("ids")]
    public List<long> Ids { get; set; } = [];

    /// <summary>
    /// Payment method accounts
    /// </summary>
    [JsonProperty("list")]
    public List<GateP2pPaymentMethodAccount> List { get; set; } = [];
}

/// <summary>
/// P2P payment method account
/// </summary>
public record GateP2pPaymentMethodAccount
{
    /// <summary>
    /// Payment account ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long? UserId { get; set; }

    /// <summary>
    /// Payment method record ID
    /// </summary>
    [JsonProperty("bankid")]
    public string BankId { get; set; }

    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickname")]
    public string Nickname { get; set; }

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
    /// Bank city
    /// </summary>
    [JsonProperty("bankcity")]
    public string BankCity { get; set; }

    /// <summary>
    /// Bank province
    /// </summary>
    [JsonProperty("bankprov")]
    public string BankProvince { get; set; }

    /// <summary>
    /// Bank address or masked card number
    /// </summary>
    [JsonProperty("bankaddr")]
    public string BankAddress { get; set; }

    /// <summary>
    /// Bank note
    /// </summary>
    [JsonProperty("bankdesc")]
    public string BankDescription { get; set; }

    /// <summary>
    /// Cardholder UID
    /// </summary>
    [JsonProperty("hold_uid")]
    public long? HolderUserId { get; set; }

    /// <summary>
    /// Cardholder user name
    /// </summary>
    [JsonProperty("hold_username")]
    public string HolderUserName { get; set; }

    /// <summary>
    /// Real name
    /// </summary>
    [JsonProperty("real_name")]
    public string RealName { get; set; }

    /// <summary>
    /// Payment account description
    /// </summary>
    [JsonProperty("account_des")]
    public string AccountDescription { get; set; }

    /// <summary>
    /// Payment type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// File link
    /// </summary>
    [JsonProperty("file")]
    public string File { get; set; }

    /// <summary>
    /// File key
    /// </summary>
    [JsonProperty("file_key")]
    public string FileKey { get; set; }

    /// <summary>
    /// Payment account
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Payment method code
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Additional memo
    /// </summary>
    [JsonProperty("memo_ext")]
    public string MemoExt { get; set; }

    /// <summary>
    /// Trading tips
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

/// <summary>
/// P2P transaction page
/// </summary>
public record GateP2pTransactionPage
{
    /// <summary>
    /// Transactions
    /// </summary>
    [JsonProperty("list")]
    public List<GateP2pTransaction> List { get; set; } = [];

    /// <summary>
    /// Countdown markers
    /// </summary>
    [JsonProperty("trans_time")]
    public List<GateP2pTransactionTimeMarker> TransactionTimes { get; set; } = [];

    /// <summary>
    /// Total count
    /// </summary>
    [JsonProperty("count")]
    public int? Count { get; set; }

    /// <summary>
    /// Exported count
    /// </summary>
    [JsonProperty("exported_num")]
    public int? ExportedNumber { get; set; }
}

/// <summary>
/// P2P transaction
/// </summary>
public record GateP2pTransaction
{
    /// <summary>
    /// Current user's side. 1: buy, 0: sell
    /// </summary>
    [JsonProperty("type_buy")]
    public int? TypeBuy { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("timest")]
    public DateTime? Time { get; set; }

    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonProperty("timest_expire")]
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Price
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
    /// Order ID
    /// </summary>
    [JsonProperty("txid")]
    public long? TransactionId { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Counterparty real name
    /// </summary>
    [JsonProperty("its_realname")]
    public string CounterpartyRealName { get; set; }

    /// <summary>
    /// Counterparty encrypted UID
    /// </summary>
    [JsonProperty("its_uid")]
    public string CounterpartyUserId { get; set; }

    /// <summary>
    /// Counterparty nickname
    /// </summary>
    [JsonProperty("its_nick")]
    public string CounterpartyNickname { get; set; }

    /// <summary>
    /// Seller real name
    /// </summary>
    [JsonProperty("seller_realname")]
    public string SellerRealName { get; set; }

    /// <summary>
    /// Buyer real name
    /// </summary>
    [JsonProperty("buyer_realname")]
    public string BuyerRealName { get; set; }

    /// <summary>
    /// Cancelable flag
    /// </summary>
    [JsonProperty("cancelable")]
    public int? Cancelable { get; set; }

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
    /// Hide payment flag
    /// </summary>
    [JsonProperty("hide_payment")]
    public int? HidePayment { get; set; }

    /// <summary>
    /// Selected payment type
    /// </summary>
    [JsonProperty("sel_paytype")]
    public string SelectedPaymentType { get; set; }

    /// <summary>
    /// Additional payment methods
    /// </summary>
    [JsonProperty("pay_others")]
    public List<GateP2pPaymentOption> OtherPaymentOptions { get; set; } = [];

    /// <summary>
    /// Countdown in seconds
    /// </summary>
    [JsonProperty("cd_time")]
    public long? CountdownTime { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("order_type")]
    public int? OrderType { get; set; }

    /// <summary>
    /// Order tags
    /// </summary>
    [JsonProperty("order_tag")]
    public List<string> OrderTags { get; set; } = [];

    /// <summary>
    /// Flash swap conversion information
    /// </summary>
    [JsonProperty("convert_info")]
    public GateP2pConvertInfo ConvertInfo { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

/// <summary>
/// P2P transaction time marker
/// </summary>
public record GateP2pTransactionTimeMarker
{
    /// <summary>
    /// Countdown time
    /// </summary>
    [JsonProperty("od_time")]
    public long? OrderTime { get; set; }
}

/// <summary>
/// P2P payment option
/// </summary>
public record GateP2pPaymentOption
{
    /// <summary>
    /// Payment method ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Payment method description
    /// </summary>
    [JsonProperty("account_des")]
    public string AccountDescription { get; set; }

    /// <summary>
    /// Payment type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// Payment name
    /// </summary>
    [JsonProperty("pay_name")]
    public string PayName { get; set; }

    /// <summary>
    /// Payment account
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Trading tips
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

/// <summary>
/// P2P flash swap conversion information
/// </summary>
public record GateP2pConvertInfo
{
    /// <summary>
    /// Target currency
    /// </summary>
    [JsonProperty("convert_type")]
    public string ConvertType { get; set; }

    /// <summary>
    /// Conversion status
    /// </summary>
    [JsonProperty("convert_status")]
    public string ConvertStatus { get; set; }

    /// <summary>
    /// Expected price
    /// </summary>
    [JsonProperty("pre_rate")]
    public decimal? PreRate { get; set; }

    /// <summary>
    /// Execution price
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Expected fiat price
    /// </summary>
    [JsonProperty("pre_fiat_rate")]
    public decimal? PreFiatRate { get; set; }

    /// <summary>
    /// Fiat execution price
    /// </summary>
    [JsonProperty("fiat_rate")]
    public decimal? FiatRate { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Swap amount
    /// </summary>
    [JsonProperty("convert_amount")]
    public decimal? ConvertAmount { get; set; }

    /// <summary>
    /// Slippage
    /// </summary>
    [JsonProperty("slippage")]
    public decimal? Slippage { get; set; }

    /// <summary>
    /// Display status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
}

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

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

/// <summary>
/// P2P ad status update result
/// </summary>
public record GateP2pAdStatusResult
{
    /// <summary>
    /// Updated ad status
    /// </summary>
    [JsonProperty("status")]
    public GateP2pAdStatusUpdate? Status { get; set; }
}

/// <summary>
/// P2P advertisement
/// </summary>
public record GateP2pAdvertisement
{
    /// <summary>
    /// Advertisement ID
    /// </summary>
    [JsonProperty("orderid")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Advertisement ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Advertisement side
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateP2pOrderSide? Type { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Original price
    /// </summary>
    [JsonProperty("original_rate")]
    public decimal? OriginalRate { get; set; }

    /// <summary>
    /// Remaining crypto amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Fiat total
    /// </summary>
    [JsonProperty("total")]
    public decimal? Total { get; set; }

    /// <summary>
    /// Trade amount text limit
    /// </summary>
    [JsonProperty("limit_total")]
    public string LimitTotal { get; set; }

    /// <summary>
    /// Fiat amount text limit
    /// </summary>
    [JsonProperty("limit_fiat")]
    public string LimitFiat { get; set; }

    /// <summary>
    /// Minimum trade amount
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal? MinimumAmount { get; set; }

    /// <summary>
    /// Maximum trade amount
    /// </summary>
    [JsonProperty("max_amount")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// Alipay flag
    /// </summary>
    [JsonProperty("pay_ali")]
    public int? PayAli { get; set; }

    /// <summary>
    /// Bank flag
    /// </summary>
    [JsonProperty("pay_bank")]
    public int? PayBank { get; set; }

    /// <summary>
    /// PayPal flag
    /// </summary>
    [JsonProperty("pay_paypal")]
    public int? PayPaypal { get; set; }

    /// <summary>
    /// WeChat flag
    /// </summary>
    [JsonProperty("pay_wechat")]
    public int? PayWechat { get; set; }

    /// <summary>
    /// Payment type numbers
    /// </summary>
    [JsonProperty("pay_type_num")]
    public string PaymentTypeNumbers { get; set; }

    /// <summary>
    /// Payment type JSON mapping
    /// </summary>
    [JsonProperty("pay_type_json")]
    public string PaymentTypeJson { get; set; }

    /// <summary>
    /// Locked amount
    /// </summary>
    [JsonProperty("locked_amount")]
    public decimal? LockedAmount { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("currency_type")]
    public string CurrencyType { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("currencyType")]
    private string CurrencyTypeAlias { set => CurrencyType = value; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("want_type")]
    public string WantType { get; set; }

    /// <summary>
    /// Hidden rate
    /// </summary>
    [JsonProperty("hide_rate")]
    public decimal? HideRate { get; set; }

    /// <summary>
    /// Trading tips
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    /// <summary>
    /// Auto reply
    /// </summary>
    [JsonProperty("auto_reply")]
    public string AutoReply { get; set; }

    /// <summary>
    /// Rate reference ID
    /// </summary>
    [JsonProperty("rate_ref_id")]
    public int? RateReferenceId { get; set; }

    /// <summary>
    /// Rate offset
    /// </summary>
    [JsonProperty("rate_offset")]
    public decimal? RateOffset { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Price type
    /// </summary>
    [JsonProperty("rate_fixed")]
    public int? RateFixed { get; set; }

    /// <summary>
    /// Floating direction
    /// </summary>
    [JsonProperty("float_trend")]
    public int? FloatTrend { get; set; }

    /// <summary>
    /// Payment timeout in minutes
    /// </summary>
    [JsonProperty("expire_min")]
    public int? ExpireMinutes { get; set; }

    /// <summary>
    /// VIP level limit
    /// </summary>
    [JsonProperty("tier_limit")]
    public int? TierLimit { get; set; }

    /// <summary>
    /// Registration age limit
    /// </summary>
    [JsonProperty("reg_time_limit")]
    public int? RegistrationTimeLimit { get; set; }

    /// <summary>
    /// Advertiser restriction
    /// </summary>
    [JsonProperty("advertisers_limit")]
    public int? AdvertisersLimit { get; set; }

    /// <summary>
    /// Verification limit
    /// </summary>
    [JsonProperty("verified_limit")]
    public int? VerifiedLimit { get; set; }

    /// <summary>
    /// Minimum completed orders limit
    /// </summary>
    [JsonProperty("min_completed_limit")]
    public int? MinimumCompletedLimit { get; set; }

    /// <summary>
    /// Maximum completed orders limit
    /// </summary>
    [JsonProperty("max_completed_limit")]
    public int? MaximumCompletedLimit { get; set; }

    /// <summary>
    /// Counterparty country limit
    /// </summary>
    [JsonProperty("user_country_limit")]
    public int? UserCountryLimit { get; set; }

    /// <summary>
    /// Counterparty concurrent order limit
    /// </summary>
    [JsonProperty("user_orders_limit")]
    public int? UserOrdersLimit { get; set; }

    /// <summary>
    /// Completion rate limit
    /// </summary>
    [JsonProperty("completed_rate_limit")]
    public decimal? CompletedRateLimit { get; set; }

    /// <summary>
    /// Chinese country limit text
    /// </summary>
    [JsonProperty("limit_country_cn")]
    public string LimitCountryChinese { get; set; }

    /// <summary>
    /// English country limit text
    /// </summary>
    [JsonProperty("limit_country_en")]
    public string LimitCountryEnglish { get; set; }

    /// <summary>
    /// Hedge flag
    /// </summary>
    [JsonProperty("is_hedge")]
    public int? IsHedge { get; set; }

    /// <summary>
    /// Hide payment flag
    /// </summary>
    [JsonProperty("hide_payment")]
    public int? HidePayment { get; set; }

    /// <summary>
    /// New hand flag
    /// </summary>
    [JsonProperty("new_hand")]
    public int? NewHand { get; set; }

    /// <summary>
    /// Out-of-time flag
    /// </summary>
    [JsonProperty("is_out_time")]
    public int? IsOutTime { get; set; }

    /// <summary>
    /// Dispute flag
    /// </summary>
    [JsonProperty("in_dispute")]
    public int? InDispute { get; set; }

    /// <summary>
    /// Advertisement timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

internal record GateP2pMyAdvertisementPage
{
    [JsonProperty("lists")]
    public List<GateP2pAdvertisement> Lists { get; set; } = [];
}

/// <summary>
/// P2P market advertisement
/// </summary>
public record GateP2pMarketAdvertisement
{
    /// <summary>
    /// Serial number
    /// </summary>
    [JsonProperty("index")]
    public int? Index { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("fiat_unit")]
    public string FiatUnit { get; set; }

    /// <summary>
    /// Advertisement ID
    /// </summary>
    [JsonProperty("adv_no")]
    public long? AdvertisementId { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Maximum crypto size per trade
    /// </summary>
    [JsonProperty("max_single_trans_amount")]
    public decimal? MaximumSingleTransactionAmount { get; set; }

    /// <summary>
    /// Minimum crypto size per trade
    /// </summary>
    [JsonProperty("min_single_trans_amount")]
    public decimal? MinimumSingleTransactionAmount { get; set; }

    /// <summary>
    /// Advertiser nickname
    /// </summary>
    [JsonProperty("nick_name")]
    public string NickName { get; set; }
}

/// <summary>
/// P2P chat history
/// </summary>
public record GateP2pChatHistory
{
    /// <summary>
    /// Messages
    /// </summary>
    [JsonProperty("messages")]
    public List<GateP2pChatMessage> Messages { get; set; } = [];
}

/// <summary>
/// P2P chat message
/// </summary>
public record GateP2pChatMessage
{
    /// <summary>
    /// Seller-side flag
    /// </summary>
    [JsonProperty("is_sell")]
    public int? IsSell { get; set; }

    /// <summary>
    /// Message type
    /// </summary>
    [JsonProperty("msg_type")]
    public int? MessageType { get; set; }

    /// <summary>
    /// Message type alias
    /// </summary>
    [JsonProperty("type")]
    public int? Type { get; set; }

    /// <summary>
    /// Message body
    /// </summary>
    [JsonProperty("msg")]
    public string Message { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }

    /// <summary>
    /// Message object
    /// </summary>
    [JsonProperty("msg_obj")]
    public JToken MessageObject { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string UserId { get; set; }

    /// <summary>
    /// Message timestamp
    /// </summary>
    [JsonProperty("timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Time { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}

/// <summary>
/// P2P chat upload result
/// </summary>
public record GateP2pChatFile
{
    /// <summary>
    /// File key
    /// </summary>
    [JsonProperty("file_key")]
    public string FileKey { get; set; }
}
