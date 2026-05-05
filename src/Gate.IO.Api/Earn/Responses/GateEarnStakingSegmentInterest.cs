namespace Gate.IO.Api.Earn;

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
