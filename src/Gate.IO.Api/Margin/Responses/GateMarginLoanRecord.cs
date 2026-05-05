namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Loan Record
/// </summary>
public record GateMarginLoanRecord
{
    /// <summary>
    /// Borrow or repay
    /// </summary>
    [JsonProperty("type")]
    public GateMarginUniOrderType Type { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Borrow or repayment amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
