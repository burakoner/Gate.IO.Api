namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginFundingBook
{
    /// <summary>
    /// Loan rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Borrowable amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// The number of days till the loan repayment&#39;s dateline
    /// </summary>
    [JsonProperty("days")]
    public int Days { get; set; }
}
