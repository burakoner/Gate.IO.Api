namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni interest record
/// </summary>
public record GateEarnUniInterestRecord
{
    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public GateEarnUniInterestRecordStatus Status { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Actual rate
    /// </summary>
    [JsonProperty("actual_rate")]
    public decimal ActualRate { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Interest status
    /// </summary>
    [JsonProperty("interest_status")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnUniInterestStatus InterestStatus { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
