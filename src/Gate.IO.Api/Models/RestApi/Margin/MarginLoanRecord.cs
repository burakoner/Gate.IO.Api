namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginLoanRecord
{
    [JsonProperty("id")]
    public long LoanRecordId { get; set; }

    [JsonProperty("loan_id")]
    public long LoanId { get; set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get;   set; }

    [JsonProperty("expire_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime ExpireTime { get;  set; }

    [JsonProperty("status"), JsonConverter(typeof(MarginLoanStatusConverter))]
    public MarginLoanStatus Status { get; set; }

    [JsonProperty("borrow_user_id")]
    public long BorrowUserId { get; set; }

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

    [JsonProperty("repaid")]
    public decimal Repaid { get; set; }

    [JsonProperty("paid_interest")]
    public decimal PaidInterest { get; set; }

    [JsonProperty("unpaid_interest")]
    public decimal UnpaidInterest { get; set; }
}