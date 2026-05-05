namespace Gate.IO.Api.Earn;

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
