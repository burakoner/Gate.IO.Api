namespace Gate.IO.Api.Unified;

/// <summary>
/// Loan history record
/// </summary>
public record GateUnifiedLoanRecord
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public GateUnifiedLoanDirection Type { get; set; }

    /// <summary>
    /// Repay type
    /// </summary>
    [JsonProperty("repayment_type")]
    public GateUnifiedRepayType? RepaymentType { get; set; }

    /// <summary>
    /// Repay type
    /// </summary>
    [JsonProperty("borrow_type")]
    public GateUnifiedBorrowType? BorrowType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
}
