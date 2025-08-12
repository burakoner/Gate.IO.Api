namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Uni Interest
/// </summary>
public record GateMarginInterest
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Actual Rate
    /// </summary>
    [JsonProperty("actual_rate")]
    public decimal ActualRate { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public GateMarginUniInterestStatus Status { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public GateMarginUniOrderType Type { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
}
