namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking coin
/// </summary>
public record GateEarnStakingCoin
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long ProductId { get; set; }

    /// <summary>
    /// Project type
    /// </summary>
    [JsonProperty("productType")]
    public int ProductType { get; set; }

    /// <summary>
    /// Whether this is a DeFi protocol
    /// </summary>
    [JsonProperty("isDefi")]
    public int IsDefi { get; set; }

    /// <summary>
    /// Staked currencies
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Estimated yield rate
    /// </summary>
    [JsonProperty("estimateApr")]
    public decimal EstimateApr { get; set; }

    /// <summary>
    /// Minimum staked amount
    /// </summary>
    [JsonProperty("minStakeAmount")]
    public decimal MinStakeAmount { get; set; }

    /// <summary>
    /// Maximum staked amount
    /// </summary>
    [JsonProperty("maxStakeAmount")]
    public decimal MaxStakeAmount { get; set; }

    /// <summary>
    /// Protocol name
    /// </summary>
    [JsonProperty("protocolName")]
    public string ProtocolName { get; set; }

    /// <summary>
    /// Redemption period in days
    /// </summary>
    [JsonProperty("redeemPeriod")]
    public int RedeemPeriod { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("exchangeRate")]
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Reverse exchange rate
    /// </summary>
    [JsonProperty("exchangeRateReserve")]
    public decimal ExchangeRateReserve { get; set; }

    /// <summary>
    /// Additional rewards
    /// </summary>
    [JsonProperty("extraInterest")]
    public List<GateEarnStakingExtraInterest> ExtraInterest { get; set; } = [];

    /// <summary>
    /// Reward currency information
    /// </summary>
    [JsonProperty("currencyRewards")]
    public List<GateEarnStakingCurrencyReward> CurrencyRewards { get; set; } = [];
}

/// <summary>
/// Staking extra interest
/// </summary>
public record GateEarnStakingExtraInterest
{
    /// <summary>
    /// Start timestamp
    /// </summary>
    [JsonProperty("start_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    [JsonProperty("end_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Additional reward currency
    /// </summary>
    [JsonProperty("reward_coin")]
    public string RewardCoin { get; set; }

    /// <summary>
    /// Tiered reward information
    /// </summary>
    [JsonProperty("segment_interest")]
    public List<GateEarnStakingSegmentInterest> SegmentInterest { get; set; } = [];
}

/// <summary>
/// Staking tiered reward
/// </summary>
public record GateEarnStakingSegmentInterest
{
    /// <summary>
    /// Tier lower value
    /// </summary>
    [JsonProperty("money_min")]
    public decimal MoneyMin { get; set; }

    /// <summary>
    /// Tier upper value
    /// </summary>
    [JsonProperty("money_max")]
    public decimal MoneyMax { get; set; }

    /// <summary>
    /// Tier interest rate
    /// </summary>
    [JsonProperty("money_rate")]
    public decimal MoneyRate { get; set; }
}

/// <summary>
/// Staking currency reward
/// </summary>
public record GateEarnStakingCurrencyReward
{
    /// <summary>
    /// Base interest rate
    /// </summary>
    [JsonProperty("apr")]
    public decimal Apr { get; set; }

    /// <summary>
    /// Reward currency
    /// </summary>
    [JsonProperty("reward_coin")]
    public string RewardCoin { get; set; }

    /// <summary>
    /// Dividend day
    /// </summary>
    [JsonProperty("reward_delay_days")]
    public int RewardDelayDays { get; set; }

    /// <summary>
    /// Interest accrual day
    /// </summary>
    [JsonProperty("interest_delay_days")]
    public int InterestDelayDays { get; set; }
}

/// <summary>
/// Staking swap result
/// </summary>
public record GateEarnStakingSwap
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long? ProductId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }

    /// <summary>
    /// Subtype
    /// </summary>
    [JsonProperty("subtype")]
    public string Subtype { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Exchange ratio
    /// </summary>
    [JsonProperty("exchange_rate")]
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Redemption amount
    /// </summary>
    [JsonProperty("exchange_amount")]
    public decimal? ExchangeAmount { get; set; }

    /// <summary>
    /// Update timestamp
    /// </summary>
    [JsonProperty("updateStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Transaction timestamp
    /// </summary>
    [JsonProperty("createStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// DeFi protocol type
    /// </summary>
    [JsonProperty("protocol_type")]
    public int? ProtocolType { get; set; }

    /// <summary>
    /// Reference ID
    /// </summary>
    [JsonProperty("client_order_id")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Order origin
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }
}

/// <summary>
/// Staking order list
/// </summary>
public record GateEarnStakingOrderPage
{
    /// <summary>
    /// Page
    /// </summary>
    [JsonProperty("page")]
    public int Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("pageCount")]
    public int PageCount { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnStakingOrder> List { get; set; } = [];
}

/// <summary>
/// Staking order
/// </summary>
public record GateEarnStakingOrder
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long ProductId { get; set; }

    /// <summary>
    /// Staked and redeemed currencies
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Redemption credit time
    /// </summary>
    [JsonProperty("redeem_stamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? RedeemTime { get; set; }

    /// <summary>
    /// Order time
    /// </summary>
    [JsonProperty("createStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Exchange amount
    /// </summary>
    [JsonProperty("exchange_amount")]
    public decimal ExchangeAmount { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}

/// <summary>
/// Staking award list
/// </summary>
public record GateEarnStakingAwardPage
{
    /// <summary>
    /// Page
    /// </summary>
    [JsonProperty("page")]
    public int Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("pageCount")]
    public int PageCount { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    /// Awards
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnStakingAward> List { get; set; } = [];
}

/// <summary>
/// Staking award
/// </summary>
public record GateEarnStakingAward
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long ProductId { get; set; }

    /// <summary>
    /// Collateral currency
    /// </summary>
    [JsonProperty("mortgage_coin")]
    public string MortgageCoin { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Reward currency
    /// </summary>
    [JsonProperty("reward_coin")]
    public string RewardCoin { get; set; }

    /// <summary>
    /// Interest amount
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    [JsonProperty("bonus_date")]
    public DateTime BonusDate { get; set; }

    /// <summary>
    /// Scheduled distribution timestamp
    /// </summary>
    [JsonProperty("should_bonus_stamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime ShouldBonusTime { get; set; }
}

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

/// <summary>
/// Staking DeFi income
/// </summary>
public record GateEarnStakingDefiIncome
{
    /// <summary>
    /// Total DeFi income
    /// </summary>
    [JsonProperty("total")]
    public List<GateEarnStakingRewardAmount> Total { get; set; } = [];
}

/// <summary>
/// Staking reward amount
/// </summary>
public record GateEarnStakingRewardAmount
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
