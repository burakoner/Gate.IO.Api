namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn subscription order
/// </summary>
public record GateEarnFixedTermLendOrder
{
    /// <summary>
    /// Subscription record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Business type
    /// </summary>
    [JsonProperty("business")]
    public int Business { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("product_id")]
    public long ProductId { get; set; }

    /// <summary>
    /// Lock-up period in days
    /// </summary>
    [JsonProperty("lock_up_period")]
    public int LockUpPeriod { get; set; }

    /// <summary>
    /// Subscription principal
    /// </summary>
    [JsonProperty("principal")]
    public decimal Principal { get; set; }

    /// <summary>
    /// Annual interest rate
    /// </summary>
    [JsonProperty("year_rate")]
    public decimal YearRate { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("product_type")]
    public int ProductType { get; set; }

    /// <summary>
    /// Accrued interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Auto-renewal status
    /// </summary>
    [JsonProperty("reinvest_status")]
    public int ReinvestStatus { get; set; }

    /// <summary>
    /// Redemption payout account type
    /// </summary>
    [JsonProperty("redeem_account_type")]
    public int RedeemAccountType { get; set; }

    /// <summary>
    /// Original order ID
    /// </summary>
    [JsonProperty("origin_order")]
    public string OriginOrder { get; set; }

    /// <summary>
    /// Redemption type
    /// </summary>
    [JsonProperty("redeem_type")]
    public int RedeemType { get; set; }

    /// <summary>
    /// Redemption time
    /// </summary>
    [JsonProperty("redeem_time")]
    public DateTime? RedeemTime { get; set; }

    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonProperty("finish_time")]
    public DateTime? FinishTime { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Annual interest rate percentage display value
    /// </summary>
    [JsonProperty("year_rate_perent")]
    public decimal YearRatePercent { get; set; }

    /// <summary>
    /// Comprehensive annualized yield percentage
    /// </summary>
    [JsonProperty("total_year_rate_percent")]
    public decimal TotalYearRatePercent { get; set; }

    /// <summary>
    /// Total earnings
    /// </summary>
    [JsonProperty("total_interest")]
    public decimal TotalInterest { get; set; }

    /// <summary>
    /// Product configuration
    /// </summary>
    [JsonProperty("product_info")]
    public GateEarnFixedTermProductInfo ProductInfo { get; set; }

    /// <summary>
    /// Bonus reward campaign information
    /// </summary>
    [JsonProperty("bonus_info")]
    public GateEarnFixedTermBonusInfo BonusInfo { get; set; }

    /// <summary>
    /// Interest rate boost coupon information
    /// </summary>
    [JsonProperty("coupon_info")]
    public GateEarnFixedTermCouponInfo CouponInfo { get; set; }

    /// <summary>
    /// Redemption timestamp
    /// </summary>
    [JsonProperty("redeem_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? RedeemAt { get; set; }

    /// <summary>
    /// Expiration timestamp
    /// </summary>
    [JsonProperty("finish_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FinishAt { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("create_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateAt { get; set; }

    /// <summary>
    /// Currency icon URL
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }
}
