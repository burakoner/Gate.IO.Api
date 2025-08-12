namespace Gate.IO.Api.Unified;

/// <summary>
/// Loan info
/// </summary>
public record GateUnifiedLoan
{
    /// <summary>
    /// Asset name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Loan type
    /// </summary>
    [JsonProperty("type")]
    public GateUnifiedLoanType Type { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Last update time
    /// </summary>
    [JsonProperty("update_time")]
    public DateTime? UpdateTime { get; set; }
}
