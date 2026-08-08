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
    /// Funding rate. For Deribit, this is the current real-time rate calculated over an 8-hour period.
    /// </summary>
    [JsonProperty("funding_rate"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? FundingRate { get; set; }

    /// <summary>
    /// Funding interval in seconds
    /// </summary>
    [JsonProperty("funding_interval")]
    public int? FundingInterval { get; set; }

    /// <summary>
    /// Next funding time, returned by the API as a Unix timestamp in milliseconds
    /// </summary>
    [JsonProperty("funding_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FundingTime { get; set; }
}
