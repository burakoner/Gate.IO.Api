namespace Gate.IO.Api.Earn;

internal record GateEarnFixedTermResponse<T> where T : class
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }
}

/// <summary>
/// Fixed-term Earn product list
/// </summary>
public record GateEarnFixedTermProductPage
{
    /// <summary>
    /// Product list
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermProduct> List { get; set; } = [];

    /// <summary>
    /// Total records
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}

/// <summary>
/// Fixed-term Earn product
/// </summary>
public record GateEarnFixedTermProduct
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Lock-up period in days
    /// </summary>
    [JsonProperty("lock_up_period")]
    public int LockUpPeriod { get; set; }

    /// <summary>
    /// Minimum earn amount
    /// </summary>
    [JsonProperty("min_lend_amount")]
    public decimal MinLendAmount { get; set; }

    /// <summary>
    /// User maximum earn limit
    /// </summary>
    [JsonProperty("user_max_lend_amount")]
    public decimal UserMaxLendAmount { get; set; }

    /// <summary>
    /// Platform earn limit
    /// </summary>
    [JsonProperty("total_lend_amount")]
    public decimal TotalLendAmount { get; set; }

    /// <summary>
    /// Annual interest rate
    /// </summary>
    [JsonProperty("year_rate")]
    public decimal YearRate { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("type")]
    public int? Type { get; set; }

    /// <summary>
    /// Whether early redemption is supported
    /// </summary>
    [JsonProperty("pre_redeem")]
    public int? PreRedeem { get; set; }

    /// <summary>
    /// Whether auto-renewal is supported
    /// </summary>
    [JsonProperty("reinvest")]
    public int? Reinvest { get; set; }

    /// <summary>
    /// Whether fixed-to-flexible conversion is supported
    /// </summary>
    [JsonProperty("redeem_account")]
    public int? RedeemAccount { get; set; }

    /// <summary>
    /// Minimum VIP level requirement
    /// </summary>
    [JsonProperty("min_vip")]
    public int? MinVip { get; set; }

    /// <summary>
    /// Maximum VIP level requirement
    /// </summary>
    [JsonProperty("max_vip")]
    public int? MaxVip { get; set; }

    /// <summary>
    /// Product status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// User maximum earn amount
    /// </summary>
    [JsonProperty("user_max_lend_volume")]
    public decimal? UserMaxLendVolume { get; set; }

    /// <summary>
    /// Total amount the user has invested
    /// </summary>
    [JsonProperty("user_total_amount")]
    public decimal UserTotalAmount { get; set; }

    /// <summary>
    /// Sale status
    /// </summary>
    [JsonProperty("sale_status")]
    public int? SaleStatus { get; set; }
}

/// <summary>
/// Compact fixed-term Earn product list
/// </summary>
public record GateEarnFixedTermProductSimpleList
{
    /// <summary>
    /// Product list
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermProductSimple> List { get; set; } = [];
}

/// <summary>
/// Compact fixed-term Earn product
/// </summary>
public record GateEarnFixedTermProductSimple
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Lock-up period in days
    /// </summary>
    [JsonProperty("lock_up_period")]
    public int LockUpPeriod { get; set; }

    /// <summary>
    /// Annual interest rate
    /// </summary>
    [JsonProperty("year_rate")]
    public decimal YearRate { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("type")]
    public int? Type { get; set; }

    /// <summary>
    /// Whether early redemption is supported
    /// </summary>
    [JsonProperty("pre_redeem")]
    public int? PreRedeem { get; set; }

    /// <summary>
    /// Whether auto-renewal is supported
    /// </summary>
    [JsonProperty("reinvest")]
    public int? Reinvest { get; set; }

    /// <summary>
    /// Whether fixed-to-flexible conversion is supported
    /// </summary>
    [JsonProperty("simple_earn")]
    public int? SimpleEarn { get; set; }

    /// <summary>
    /// Minimum VIP level requirement
    /// </summary>
    [JsonProperty("min_vip")]
    public int? MinVip { get; set; }

    /// <summary>
    /// Maximum VIP level requirement
    /// </summary>
    [JsonProperty("max_vip")]
    public int? MaxVip { get; set; }

    /// <summary>
    /// Sale status
    /// </summary>
    [JsonProperty("sale_status")]
    public int SaleStatus { get; set; }
}

/// <summary>
/// Fixed-term Earn subscription order list
/// </summary>
public record GateEarnFixedTermLendPage
{
    /// <summary>
    /// Subscription order list
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermLendOrder> List { get; set; } = [];

    /// <summary>
    /// Total records
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}

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

/// <summary>
/// Fixed-term Earn product configuration
/// </summary>
public record GateEarnFixedTermProductInfo
{
    /// <summary>
    /// Whether early redemption is supported
    /// </summary>
    [JsonProperty("pre_redeem")]
    public int PreRedeem { get; set; }

    /// <summary>
    /// Whether auto-renewal is supported
    /// </summary>
    [JsonProperty("reinvest")]
    public int Reinvest { get; set; }

    /// <summary>
    /// Redemption payout account type
    /// </summary>
    [JsonProperty("redeem_account")]
    public int RedeemAccount { get; set; }

    /// <summary>
    /// Minimum VIP level requirement
    /// </summary>
    [JsonProperty("min_vip")]
    public int MinVip { get; set; }

    /// <summary>
    /// Maximum VIP level requirement
    /// </summary>
    [JsonProperty("max_vip")]
    public int MaxVip { get; set; }
}

/// <summary>
/// Fixed-term Earn bonus campaign information
/// </summary>
public record GateEarnFixedTermBonusInfo
{
    /// <summary>
    /// Activity ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Associated product ID
    /// </summary>
    [JsonProperty("product_id")]
    public long ProductId { get; set; }

    /// <summary>
    /// Product currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Reward currency
    /// </summary>
    [JsonProperty("bonus_asset")]
    public string BonusAsset { get; set; }

    /// <summary>
    /// KYC level restrictions
    /// </summary>
    [JsonProperty("kyc_limit")]
    public string KycLimit { get; set; }

    /// <summary>
    /// Tiered annual interest rate
    /// </summary>
    [JsonProperty("ladder_apr")]
    public List<GateEarnFixedTermLadderApr> LadderApr { get; set; } = [];

    /// <summary>
    /// Total reward amount
    /// </summary>
    [JsonProperty("total_bonus_amount")]
    public decimal TotalBonusAmount { get; set; }

    /// <summary>
    /// Maximum reward per user
    /// </summary>
    [JsonProperty("user_total_bonus_amount")]
    public decimal UserTotalBonusAmount { get; set; }

    /// <summary>
    /// Activity status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Activity start time
    /// </summary>
    [JsonProperty("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Activity end time
    /// </summary>
    [JsonProperty("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Activity start timestamp
    /// </summary>
    [JsonProperty("start_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? StartAt { get; set; }

    /// <summary>
    /// Activity end timestamp
    /// </summary>
    [JsonProperty("end_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? EndAt { get; set; }

    /// <summary>
    /// Total rewards distributed
    /// </summary>
    [JsonProperty("total_issued_amount")]
    public decimal TotalIssuedAmount { get; set; }

    /// <summary>
    /// Total rewards distributed to the user
    /// </summary>
    [JsonProperty("user_total_issued_amount")]
    public decimal UserTotalIssuedAmount { get; set; }

    /// <summary>
    /// Reward currency price
    /// </summary>
    [JsonProperty("bonus_asset_price")]
    public decimal BonusAssetPrice { get; set; }

    /// <summary>
    /// Product currency price
    /// </summary>
    [JsonProperty("product_asset_price")]
    public decimal ProductAssetPrice { get; set; }

    /// <summary>
    /// Product base annual interest rate
    /// </summary>
    [JsonProperty("product_year_rate")]
    public decimal ProductYearRate { get; set; }
}

/// <summary>
/// Fixed-term Earn ladder APR
/// </summary>
public record GateEarnFixedTermLadderApr
{
    /// <summary>
    /// Annualized interest rate
    /// </summary>
    [JsonProperty("apr")]
    public decimal Apr { get; set; }

    /// <summary>
    /// Range lower limit
    /// </summary>
    [JsonProperty("left")]
    public decimal Left { get; set; }

    /// <summary>
    /// Range upper limit
    /// </summary>
    [JsonProperty("right")]
    public decimal Right { get; set; }
}

/// <summary>
/// Fixed-term Earn coupon information
/// </summary>
public record GateEarnFixedTermCouponInfo
{
    /// <summary>
    /// Interest rate boost coupon record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Business type
    /// </summary>
    [JsonProperty("business")]
    public int Business { get; set; }

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
    /// Associated order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Interest rate boost coupon ID
    /// </summary>
    [JsonProperty("financial_rate_id")]
    public long FinancialRateId { get; set; }

    /// <summary>
    /// Minimum subscription amount
    /// </summary>
    [JsonProperty("buy_limit_low")]
    public decimal BuyLimitLow { get; set; }

    /// <summary>
    /// Maximum subscription amount
    /// </summary>
    [JsonProperty("buy_limit_high")]
    public decimal BuyLimitHigh { get; set; }

    /// <summary>
    /// Interest rate boost days
    /// </summary>
    [JsonProperty("rate_day")]
    public int RateDay { get; set; }

    /// <summary>
    /// Interest rate boost percentage
    /// </summary>
    [JsonProperty("rate_ratio")]
    public decimal RateRatio { get; set; }

    /// <summary>
    /// Actual interest rate boost days
    /// </summary>
    [JsonProperty("coupon_days")]
    public int CouponDays { get; set; }

    /// <summary>
    /// Principal for interest rate boost calculation
    /// </summary>
    [JsonProperty("coupon_principal")]
    public decimal CouponPrincipal { get; set; }

    /// <summary>
    /// Interest rate boost APR
    /// </summary>
    [JsonProperty("coupon_year_rate")]
    public decimal CouponYearRate { get; set; }

    /// <summary>
    /// Interest generated from rate boost
    /// </summary>
    [JsonProperty("coupon_interest")]
    public decimal CouponInterest { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Settlement time
    /// </summary>
    [JsonProperty("finish_time")]
    public DateTime? FinishTime { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }
}

/// <summary>
/// Fixed-term Earn subscription result
/// </summary>
public record GateEarnFixedTermLendResult
{
    /// <summary>
    /// Subscription order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
}

/// <summary>
/// Fixed-term Earn history page
/// </summary>
public record GateEarnFixedTermHistoryPage
{
    /// <summary>
    /// History records
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermHistoryRecord> List { get; set; } = [];

    /// <summary>
    /// Total records
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}

/// <summary>
/// Fixed-term Earn history record
/// </summary>
public record GateEarnFixedTermHistoryRecord
{
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

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
    /// Unique time identifier
    /// </summary>
    [JsonProperty("uniq_time")]
    public string UniqueTime { get; set; }

    /// <summary>
    /// Reward campaign ID
    /// </summary>
    [JsonProperty("bonus_id")]
    public long? BonusId { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("product_id")]
    public long ProductId { get; set; }

    /// <summary>
    /// Reward currency
    /// </summary>
    [JsonProperty("bonus_asset")]
    public string BonusAsset { get; set; }

    /// <summary>
    /// Total principal
    /// </summary>
    [JsonProperty("total_principal")]
    public decimal TotalPrincipal { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency price
    /// </summary>
    [JsonProperty("asset_price")]
    public decimal AssetPrice { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Detail description
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("create_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateAt { get; set; }

    /// <summary>
    /// Term
    /// </summary>
    [JsonProperty("lock_up_period")]
    public int LockUpPeriod { get; set; }
}
