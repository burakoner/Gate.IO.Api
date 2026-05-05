namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment option type
/// </summary>
public enum GateEarnDualOptionType : byte
{
    /// <summary>
    /// Buy low
    /// </summary>
    [Map("put")]
    Put = 1,

    /// <summary>
    /// Sell high
    /// </summary>
    [Map("call")]
    Call = 2,
}

/// <summary>
/// Dual investment product sort
/// </summary>
public enum GateEarnDualPlanSort : byte
{
    /// <summary>
    /// Highest APY first
    /// </summary>
    [Map("apy")]
    Apy = 1,

    /// <summary>
    /// Shortest tenor first
    /// </summary>
    [Map("short-period")]
    ShortPeriod = 2,

    /// <summary>
    /// Highest premium first
    /// </summary>
    [Map("multiple")]
    Multiple = 3,
}

/// <summary>
/// Dual investment order query status
/// </summary>
public enum GateEarnDualOrderQueryStatus : byte
{
    /// <summary>
    /// Open position
    /// </summary>
    [Map("HOLD")]
    Hold = 1,

    /// <summary>
    /// Historical position
    /// </summary>
    [Map("REPAY")]
    Repay = 2,

    /// <summary>
    /// Position active
    /// </summary>
    [Map("PROCESSING")]
    Processing = 3,

    /// <summary>
    /// Settlement in progress
    /// </summary>
    [Map("SETTLEMENT_PROCESSING")]
    SettlementProcessing = 4,

    /// <summary>
    /// All orders
    /// </summary>
    [Map("ALL")]
    All = 5,
}

/// <summary>
/// Dual investment recommendation mode
/// </summary>
public enum GateEarnDualRecommendationMode : byte
{
    /// <summary>
    /// Normal recommendation
    /// </summary>
    [Map("normal")]
    Normal = 1,

    /// <summary>
    /// Curated picks
    /// </summary>
    [Map("senior")]
    Senior = 2,

    /// <summary>
    /// APY ascending
    /// </summary>
    [Map("apy_up")]
    ApyAscending = 3,

    /// <summary>
    /// Target price descending
    /// </summary>
    [Map("ep_down")]
    ExercisePriceDescending = 4,

    /// <summary>
    /// Target price ascending
    /// </summary>
    [Map("ep_up")]
    ExercisePriceAscending = 5,

    /// <summary>
    /// Maturity time descending
    /// </summary>
    [Map("dt_down")]
    DeliveryTimeDescending = 6,

    /// <summary>
    /// Maturity time ascending
    /// </summary>
    [Map("dt_up")]
    DeliveryTimeAscending = 7,
}

/// <summary>
/// Staking currency type
/// </summary>
public enum GateEarnStakingCoinType : byte
{
    /// <summary>
    /// Voucher
    /// </summary>
    [Map("swap")]
    Swap = 1,

    /// <summary>
    /// Locked position
    /// </summary>
    [Map("lock")]
    Lock = 2,

    /// <summary>
    /// US Treasury bond
    /// </summary>
    [Map("debt")]
    Debt = 3,
}

/// <summary>
/// Staking operation type
/// </summary>
public enum GateEarnStakingOperationType : byte
{
    /// <summary>
    /// Stake
    /// </summary>
    Stake = 0,

    /// <summary>
    /// Redeem
    /// </summary>
    Redeem = 1,
}

/// <summary>
/// Auto invest period type
/// </summary>
public enum GateEarnAutoInvestPeriodType : byte
{
    /// <summary>
    /// Daily
    /// </summary>
    [Map("daily")]
    Daily = 1,

    /// <summary>
    /// Weekly
    /// </summary>
    [Map("weekly")]
    Weekly = 2,

    /// <summary>
    /// Biweekly
    /// </summary>
    [Map("biweekly")]
    Biweekly = 3,

    /// <summary>
    /// Monthly
    /// </summary>
    [Map("monthly")]
    Monthly = 4,

    /// <summary>
    /// Hourly
    /// </summary>
    [Map("hourly")]
    Hourly = 5,

    /// <summary>
    /// Every four hours
    /// </summary>
    [Map("4-hourly")]
    FourHourly = 6,
}

/// <summary>
/// Auto invest fund source
/// </summary>
public enum GateEarnAutoInvestFundSource : byte
{
    /// <summary>
    /// Spot account
    /// </summary>
    [Map("spot")]
    Spot = 1,

    /// <summary>
    /// Flexible savings
    /// </summary>
    [Map("earn")]
    Earn = 2,
}

/// <summary>
/// Auto invest fund flow direction
/// </summary>
public enum GateEarnAutoInvestFundFlow : byte
{
    /// <summary>
    /// Auto invest
    /// </summary>
    [Map("auto_invest")]
    AutoInvest = 1,

    /// <summary>
    /// Flexible savings
    /// </summary>
    [Map("earn")]
    Earn = 2,
}

/// <summary>
/// Auto invest plan status
/// </summary>
public enum GateEarnAutoInvestPlanStatus : byte
{
    /// <summary>
    /// Active plans
    /// </summary>
    [Map("active")]
    Active = 1,

    /// <summary>
    /// Historical plans
    /// </summary>
    [Map("history")]
    History = 2,
}

/// <summary>
/// Auto invest creation type
/// </summary>
public enum GateEarnAutoInvestCreationType : byte
{
    /// <summary>
    /// Normal creation
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Quick investment
    /// </summary>
    QuickInvestment = 1,
}

/// <summary>
/// Fixed-term Earn product type
/// </summary>
public enum GateEarnFixedTermProductType : byte
{
    /// <summary>
    /// All products
    /// </summary>
    All = 0,

    /// <summary>
    /// Regular product
    /// </summary>
    Regular = 1,

    /// <summary>
    /// VIP product
    /// </summary>
    Vip = 2,
}

/// <summary>
/// Fixed-term Earn order type
/// </summary>
public enum GateEarnFixedTermOrderType : byte
{
    /// <summary>
    /// Current orders
    /// </summary>
    Current = 1,

    /// <summary>
    /// Historical orders
    /// </summary>
    Historical = 2,
}

/// <summary>
/// Fixed-term Earn history type
/// </summary>
public enum GateEarnFixedTermHistoryType : byte
{
    /// <summary>
    /// Subscription
    /// </summary>
    Subscription = 1,

    /// <summary>
    /// Redemption
    /// </summary>
    Redemption = 2,

    /// <summary>
    /// Interest
    /// </summary>
    Interest = 3,

    /// <summary>
    /// Bonus reward
    /// </summary>
    BonusReward = 4,
}
