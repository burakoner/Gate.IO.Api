namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Cross Margin Loan
/// </summary>
public class GateCrossMarginLoan
{
    /// <summary>
    /// Loan record ID
    /// </summary>
    [JsonProperty("id")]
    public long LoanId { get; set; }

    /// <summary>
    /// Creation timestamp, in milliseconds
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update timestamp, in milliseconds
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Repaid amount
    /// </summary>
    [JsonProperty("repaid")]
    public decimal Repaid { get; set; }

    /// <summary>
    /// Repaid interest
    /// </summary>
    [JsonProperty("repaid_interest")]
    public decimal RepaidInterest { get; set; }

    /// <summary>
    /// Outstanding interest yet to be paid
    /// </summary>
    [JsonProperty("unpaid_interest")]
    public decimal UnpaidInterest { get; set; }
}