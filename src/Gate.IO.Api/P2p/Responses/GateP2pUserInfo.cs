namespace Gate.IO.Api.P2p;

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

    /// <summary>
    /// Gets or sets the Additional Data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
