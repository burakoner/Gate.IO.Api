namespace Gate.IO.Api.Earn;

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
