namespace Gate.IO.Api.Earn;

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
