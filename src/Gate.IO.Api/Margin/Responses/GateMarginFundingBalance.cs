namespace Gate.IO.Api.Margin;

/// <summary>
/// GateMarginFundingBalance
/// </summary>
public class GateMarginFundingBalance
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Available assets to lend, which is identical to spot account available
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount. i.e. amount in open loans
    /// </summary>
    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    /// <summary>
    /// Outstanding loan amount yet to be repaid
    /// </summary>
    [JsonProperty("lent")]
    public decimal Lent { get; set; }

    /// <summary>
    /// Amount used for lending. total_lent = lent + locked
    /// </summary>
    [JsonProperty("total_lent")]
    public decimal TotalLent { get; set; }
}
