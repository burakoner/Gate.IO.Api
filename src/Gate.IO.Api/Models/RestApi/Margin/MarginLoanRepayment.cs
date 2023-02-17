namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginLoanRepayment
{
    /// <summary>
    /// Loan record ID
    /// </summary>
    [JsonProperty("id")]
    public long RepaymentId { get; set; }

    /// <summary>
    /// Repayment time
    /// </summary>
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

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
}
