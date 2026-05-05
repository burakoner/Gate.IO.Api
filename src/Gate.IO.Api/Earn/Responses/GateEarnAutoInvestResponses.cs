namespace Gate.IO.Api.Earn;

/// <summary>
/// Created auto invest plan
/// </summary>
public record GateEarnAutoInvestPlanCreated
{
    /// <summary>
    /// Plan ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Per-period auto invest amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("money")]
    public string Money { get; set; }

    /// <summary>
    /// Next execution time
    /// </summary>
    [JsonProperty("next_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextTime { get; set; }

    /// <summary>
    /// Cycle type
    /// </summary>
    [JsonProperty("period_type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnAutoInvestPeriodType PeriodType { get; set; }

    /// <summary>
    /// Cycle day
    /// </summary>
    [JsonProperty("period_day")]
    public long PeriodDay { get; set; }

    /// <summary>
    /// Cycle hours
    /// </summary>
    [JsonProperty("period_hour")]
    public long PeriodHour { get; set; }

    /// <summary>
    /// Fund flow direction
    /// </summary>
    [JsonProperty("fund_flow")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnAutoInvestFundFlow FundFlow { get; set; }

    /// <summary>
    /// Fund source
    /// </summary>
    [JsonProperty("fund_source")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnAutoInvestFundSource FundSource { get; set; }
}

/// <summary>
/// Currency supporting auto invest
/// </summary>
public record GateEarnAutoInvestCoin
{
    /// <summary>
    /// Currency code
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <summary>
    /// Currency icon URL
    /// </summary>
    [JsonProperty("asset_icon_url")]
    public string AssetIconUrl { get; set; }

    /// <summary>
    /// Sort
    /// </summary>
    [JsonProperty("sort")]
    public long Sort { get; set; }
}

/// <summary>
/// Auto invest minimum amount
/// </summary>
public record GateEarnAutoInvestMinimumAmount
{
    /// <summary>
    /// Minimum amount
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal MinAmount { get; set; }
}

/// <summary>
/// Auto invest plan execution records
/// </summary>
public record GateEarnAutoInvestExecutionRecordPage
{
    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("page")]
    public long Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [JsonProperty("page_size")]
    public long PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("total_page")]
    public long TotalPage { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// Execution records
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnAutoInvestExecutionRecord> List { get; set; } = [];
}

/// <summary>
/// Auto invest plan execution record
/// </summary>
public record GateEarnAutoInvestExecutionRecord
{
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Source currency
    /// </summary>
    [JsonProperty("money")]
    public string Money { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Plan ID
    /// </summary>
    [JsonProperty("plan_id")]
    public long PlanId { get; set; }

    /// <summary>
    /// Plan version
    /// </summary>
    [JsonProperty("plan_version")]
    public long PlanVersion { get; set; }

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Investment time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Status enum
    /// </summary>
    [JsonProperty("status_type")]
    public long StatusType { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("side")]
    public long Side { get; set; }

    /// <summary>
    /// Status description
    /// </summary>
    [JsonProperty("status_message")]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }
}

/// <summary>
/// Auto invest order item
/// </summary>
public record GateEarnAutoInvestOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Plan ID
    /// </summary>
    [JsonProperty("plan_id")]
    public long PlanId { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("side")]
    public long Side { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("record_id")]
    public long RecordId { get; set; }

    /// <summary>
    /// Total amount
    /// </summary>
    [JsonProperty("total_money")]
    public decimal TotalMoney { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Fund flow direction
    /// </summary>
    [JsonProperty("fund_flow")]
    public string FundFlow { get; set; }

    /// <summary>
    /// Error code
    /// </summary>
    [JsonProperty("error_code")]
    public long ErrorCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    [JsonProperty("error_msg")]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public long Status { get; set; }
}

/// <summary>
/// Auto invest currency configuration
/// </summary>
public record GateEarnAutoInvestConfig
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Investment limit
    /// </summary>
    [JsonProperty("max_limit")]
    public decimal MaxLimit { get; set; }
}

/// <summary>
/// Auto invest plan list
/// </summary>
public record GateEarnAutoInvestPlanPage
{
    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("page")]
    public long Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [JsonProperty("page_size")]
    public long PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("page_count")]
    public long PageCount { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("total_count")]
    public long TotalCount { get; set; }

    /// <summary>
    /// Plans
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnAutoInvestPlan> List { get; set; } = [];
}

/// <summary>
/// Auto invest plan details
/// </summary>
public record GateEarnAutoInvestPlan
{
    /// <summary>
    /// Plan ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Plan version
    /// </summary>
    [JsonProperty("version")]
    public long Version { get; set; }

    /// <summary>
    /// Plan name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("money")]
    public string Money { get; set; }

    /// <summary>
    /// Per-period investment amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Cycle type
    /// </summary>
    [JsonProperty("period_type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnAutoInvestPeriodType PeriodType { get; set; }

    /// <summary>
    /// Cycle day
    /// </summary>
    [JsonProperty("period_day")]
    public long PeriodDay { get; set; }

    /// <summary>
    /// Cycle hours
    /// </summary>
    [JsonProperty("period_hour")]
    public long PeriodHour { get; set; }

    /// <summary>
    /// Investment portfolio
    /// </summary>
    [JsonProperty("portfolio")]
    public List<GateEarnAutoInvestPortfolioPosition> Portfolio { get; set; } = [];

    /// <summary>
    /// Next execution time
    /// </summary>
    [JsonProperty("next_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextTime { get; set; }

    /// <summary>
    /// Executed periods
    /// </summary>
    [JsonProperty("period")]
    public long Period { get; set; }

    /// <summary>
    /// Fund source
    /// </summary>
    [JsonProperty("fund_source")]
    public string FundSource { get; set; }

    /// <summary>
    /// Fund flow direction
    /// </summary>
    [JsonProperty("fund_flow")]
    public string FundFlow { get; set; }
}

/// <summary>
/// Auto invest plan portfolio position
/// </summary>
public record GateEarnAutoInvestPortfolioPosition
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Ratio
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }

    /// <summary>
    /// Accumulated investment
    /// </summary>
    [JsonProperty("cum_invest")]
    public decimal CumInvest { get; set; }

    /// <summary>
    /// Accumulated position
    /// </summary>
    [JsonProperty("cum_hold")]
    public decimal CumHold { get; set; }

    /// <summary>
    /// Accumulated redemption
    /// </summary>
    [JsonProperty("cum_redeem")]
    public decimal CumRedeem { get; set; }

    /// <summary>
    /// Average cost price
    /// </summary>
    [JsonProperty("avg_price")]
    public decimal AveragePrice { get; set; }

    /// <summary>
    /// Redemption status
    /// </summary>
    [JsonProperty("redeem_status")]
    public long RedeemStatus { get; set; }

    /// <summary>
    /// Lending quantity
    /// </summary>
    [JsonProperty("lend_amount")]
    public decimal LendAmount { get; set; }
}
