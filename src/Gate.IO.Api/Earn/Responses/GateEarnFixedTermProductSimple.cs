namespace Gate.IO.Api.Earn;

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
