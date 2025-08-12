namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Uni Loan
/// </summary>
public record GateMarginLoan
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
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

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

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    public DateTime? UpdateTime { get; set; }
}
