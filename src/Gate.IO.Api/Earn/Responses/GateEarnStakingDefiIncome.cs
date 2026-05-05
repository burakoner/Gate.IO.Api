namespace Gate.IO.Api.Earn;

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
