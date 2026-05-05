namespace Gate.IO.Api.Earn;

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
