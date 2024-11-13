namespace Gate.IO.Api.Margin;

/// <summary>
/// GateCrossMarginLoanRepayment
/// </summary>
public class GateCrossMarginRepayment
{
    /// <summary>
    /// Loan record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
    
    /// <summary>
    /// Repayment time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Loan record ID
    /// </summary>
    [JsonProperty("loan_id")]
    public long LoanId { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Repaid principal
    /// </summary>
    [JsonProperty("principal")]
    public decimal Principal { get; set; }

    /// <summary>
    /// Repaid interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Repayment type: none - no repayment type, manual_repay - manual repayment, auto_repay - automatic repayment, cancel_auto_repay - automatic repayment after cancellation
    /// </summary>
    [JsonProperty("repayment_type")]
    public GateCrossMarginRepaymentType RepaymentType { get; set; }
}