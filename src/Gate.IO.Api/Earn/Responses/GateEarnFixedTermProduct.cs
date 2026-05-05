namespace Gate.IO.Api.Earn;

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
