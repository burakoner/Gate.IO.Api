namespace Gate.IO.Api.Earn;

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
    /// Distribution status
    /// </summary>
    [JsonProperty("status")]
    public GateEarnStakingAwardStatus Status { get; set; }

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
