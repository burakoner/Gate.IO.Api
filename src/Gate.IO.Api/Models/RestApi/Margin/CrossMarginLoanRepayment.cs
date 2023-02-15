namespace Gate.IO.Api.Models.RestApi.Margin;

public class CrossMarginLoanRepayment
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}

public class CrossMarginLoanRepaymentRecord
{
    [JsonProperty("id")]
    public long RepaymentId { get; set; }

    [JsonProperty("loan_id")]
    public long LoanId { get; set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("principal")]
    public decimal Principal { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}