namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx futures funding rate information
/// </summary>
public record GateCrossExMarketFundingInfo
{
    /// <summary>
    /// Trading pair
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Funding rate
    /// </summary>
    [JsonProperty("funding_rate"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? FundingRate { get; set; }

    /// <summary>
    /// Funding interval in seconds
    /// </summary>
    [JsonProperty("funding_interval")]
    public int? FundingInterval { get; set; }

    /// <summary>
    /// Next funding time
    /// </summary>
    [JsonProperty("funding_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FundingTime { get; set; }
}
