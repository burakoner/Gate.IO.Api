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
