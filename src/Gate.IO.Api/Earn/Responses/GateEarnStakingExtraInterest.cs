namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking extra interest
/// </summary>
public record GateEarnStakingExtraInterest
{
    /// <summary>
    /// Start timestamp
    /// </summary>
    [JsonProperty("start_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    [JsonProperty("end_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Additional reward currency
    /// </summary>
    [JsonProperty("reward_coin")]
    public string RewardCoin { get; set; }

    /// <summary>
    /// Tiered reward information
    /// </summary>
    [JsonProperty("segment_interest")]
    public List<GateEarnStakingSegmentInterest> SegmentInterest { get; set; } = [];
}
