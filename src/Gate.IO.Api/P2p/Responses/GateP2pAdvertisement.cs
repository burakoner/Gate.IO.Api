namespace Gate.IO.Api.P2p;

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
    /// Minimum cryptocurrency quantity per order
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal? MinimumAmount { get; set; }

    /// <summary>
    /// Maximum cryptocurrency quantity per order
    /// </summary>
    [JsonProperty("max_amount")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// Minimum fiat amount per order
    /// </summary>
    [JsonProperty("fiat_min_amount")]
    public decimal? MinimumFiatAmount { get; set; }

    /// <summary>
    /// Maximum fiat amount per order
    /// </summary>
    [JsonProperty("fiat_max_amount")]
    public decimal? MaximumFiatAmount { get; set; }

    /// <summary>
    /// Trading-limit unit
    /// </summary>
    [JsonProperty("limit_basis")]
    public GateP2pAdLimitBasis? LimitBasis { get; set; }

    /// <summary>
    /// Trading-limit unit label
    /// </summary>
    [JsonProperty("limit_basis_text")]
    public string LimitBasisText { get; set; }

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
    /// Whether trading with Polymarket users is restricted
    /// </summary>
    [JsonProperty("polymarket_limit")]
    public int? PolymarketLimit { get; set; }

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

    /// <summary>
    /// Gets or sets the Additional Data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
