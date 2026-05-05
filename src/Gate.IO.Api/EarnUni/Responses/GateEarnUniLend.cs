namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending order
/// </summary>
public record GateEarnUniLend
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Current amount
    /// </summary>
    [JsonProperty("current_amount")]
    public decimal CurrentAmount { get; set; }

    /// <summary>
    /// Total lending amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Lent amount
    /// </summary>
    [JsonProperty("lent_amount")]
    public decimal LentAmount { get; set; }

    /// <summary>
    /// Pending redemption amount
    /// </summary>
    [JsonProperty("frozen_amount")]
    public decimal FrozenAmount { get; set; }

    /// <summary>
    /// Minimum interest rate
    /// </summary>
    [JsonProperty("min_rate")]
    public decimal MinimumRate { get; set; }

    /// <summary>
    /// Interest status
    /// </summary>
    [JsonProperty("interest_status")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnUniInterestStatus InterestStatus { get; set; }

    /// <summary>
    /// Non-reinvested amount
    /// </summary>
    [JsonProperty("reinvest_left_amount")]
    public decimal ReinvestLeftAmount { get; set; }

    /// <summary>
    /// Lending order creation time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Lending order last update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }
}
