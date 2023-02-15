namespace Gate.IO.Api.Models.RestApi.Margin;

public class LoanResponse
{
    [JsonProperty("id")]
    public long LoanId { get;  set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get;   set; }

    [JsonProperty("expire_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime ExpireTime { get;  set; }

    [JsonProperty("side"), JsonConverter(typeof(MarginLoanSideConverter))]
    public MarginLoanSide Side { get; set; }

    [JsonProperty("status"), JsonConverter(typeof(MarginLoanStatusConverter))]
    public MarginLoanStatus Status { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("days")]
    public int Days { get; set; }

    [JsonProperty("auto_renew")]
    public bool AutoRenew { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("left")]
    public decimal Left { get; set; }

    [JsonProperty("repaid")]
    public decimal Repaid { get; set; }

    [JsonProperty("paid_interest")]
    public decimal PaidInterest { get; set; }

    [JsonProperty("unpaid_interest")]
    public decimal UnpaidInterest { get; set; }

    [JsonProperty("fee_rate")]
    public decimal FeeRate { get; set; }

    [JsonProperty("orig_id", NullValueHandling = NullValueHandling.Ignore)]
    public long OriginalId { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }
}