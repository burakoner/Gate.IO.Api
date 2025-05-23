namespace Gate.IO.Api.Unified;

/// <summary>
/// Interest record
/// </summary>
public record GateUnifiedInterestRecord
{
    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public bool Success { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Actual interest rate
    /// </summary>
    [JsonProperty("actual_rate")]
    public decimal ActualRate { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Loan type
    /// </summary>
    [JsonProperty("type")]
    public GateUnifiedLoanType Type { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime Timestamp { get; set; }
}
