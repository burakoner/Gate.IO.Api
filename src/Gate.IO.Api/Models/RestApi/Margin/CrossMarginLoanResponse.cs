namespace Gate.IO.Api.Models.RestApi.Margin;

public class CrossMarginLoanResponse
{
    [JsonProperty("id")]
    public long LoanId { get; set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("update_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

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

    [JsonProperty("status"), JsonConverter(typeof(CrossMarginLoanStatusConverter))]
    public CrossMarginLoanStatus Status { get; set; }

    [JsonProperty("repaid")]
    public decimal Repaid { get; set; }

    [JsonProperty("repaid_interest")]
    public decimal RepaidInterest { get; set; }

    [JsonProperty("unpaid_interest")]
    public decimal UnpaidInterest { get; set; }
}