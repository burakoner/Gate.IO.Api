namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan portfolio position
/// </summary>
public record GateEarnAutoInvestPortfolioPosition
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Ratio
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }

    /// <summary>
    /// Accumulated investment
    /// </summary>
    [JsonProperty("cum_invest")]
    public decimal CumInvest { get; set; }

    /// <summary>
    /// Accumulated position
    /// </summary>
    [JsonProperty("cum_hold")]
    public decimal CumHold { get; set; }

    /// <summary>
    /// Accumulated redemption
    /// </summary>
    [JsonProperty("cum_redeem")]
    public decimal CumRedeem { get; set; }

    /// <summary>
    /// Average cost price
    /// </summary>
    [JsonProperty("avg_price")]
    public decimal AveragePrice { get; set; }

    /// <summary>
    /// Redemption status
    /// </summary>
    [JsonProperty("redeem_status")]
    public long RedeemStatus { get; set; }

    /// <summary>
    /// Lending quantity
    /// </summary>
    [JsonProperty("lend_amount")]
    public decimal LendAmount { get; set; }
}
