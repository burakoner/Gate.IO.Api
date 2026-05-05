namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking asset
/// </summary>
public record GateEarnStakingAsset
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long ProductId { get; set; }

    /// <summary>
    /// Staked currencies
    /// </summary>
    [JsonProperty("mortgage_coin")]
    public string MortgageCoin { get; set; }

    /// <summary>
    /// Position amount
    /// </summary>
    [JsonProperty("mortgage_amount")]
    public decimal MortgageAmount { get; set; }

    /// <summary>
    /// First timestamp
    /// </summary>
    [JsonProperty("createStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Additional rewards converted to USDT amount
    /// </summary>
    [JsonProperty("extra_income")]
    public decimal ExtraIncome { get; set; }

    /// <summary>
    /// Locked amount
    /// </summary>
    [JsonProperty("freeze_amount")]
    public decimal FreezeAmount { get; set; }

    /// <summary>
    /// Move income
    /// </summary>
    [JsonProperty("move_income")]
    public decimal MoveIncome { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Total earnings by currency
    /// </summary>
    [JsonProperty("income_total")]
    public decimal IncomeTotal { get; set; }

    /// <summary>
    /// Yesterday's earnings
    /// </summary>
    [JsonProperty("yesterday_income_multi")]
    public List<GateEarnStakingRewardAmount> YesterdayIncomeMulti { get; set; } = [];

    /// <summary>
    /// Currency-specific reward earnings
    /// </summary>
    [JsonProperty("reward_coins")]
    public List<GateEarnStakingCurrencyReward> RewardCoins { get; set; } = [];

    /// <summary>
    /// DeFi earnings
    /// </summary>
    [JsonProperty("defi_income")]
    public GateEarnStakingDefiIncome DefiIncome { get; set; }
}
