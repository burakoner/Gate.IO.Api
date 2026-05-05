namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account borrowing and repayment response result
/// </summary>
public record GateUnifiedLoanResult
{
    /// <summary>
    /// Transaction ID
    /// </summary>
    [JsonProperty("tran_id")]
    public long TransactionId { get; set; }
}
