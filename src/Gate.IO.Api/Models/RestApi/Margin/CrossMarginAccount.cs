namespace Gate.IO.Api.Models.RestApi.Margin;

public class CrossMarginAccount
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Whether account is locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Gets or Sets Balances
    /// </summary>
    [JsonProperty("balances")]
    public Dictionary<string, CrossMarginBalance> Balances { get; set; }

    /// <summary>
    /// Total account value in USDT, i.e., the sum of all currencies&#39; &#x60;(available+freeze)*price*discount&#x60;
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Total borrowed value in USDT, i.e., the sum of all currencies&#39; &#x60;borrowed*price*discount&#x60;
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Total unpaid interests in USDT, i.e., the sum of all currencies&#39; &#x60;interest*price*discount&#x60;
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Risk rate. When it belows 110%, liquidation will be triggered. Calculation formula: &#x60;total / (borrowed+interest)&#x60;
    /// </summary>
    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    /// <summary>
    /// Total initial margin
    /// </summary>
    [JsonProperty("total_initial_margin")]
    public decimal TotalInitialMargin { get; set; }

    /// <summary>
    /// Total margin balance
    /// </summary>
    [JsonProperty("total_margin_balance")]
    public decimal TotalMarginBalance { get; set; }

    /// <summary>
    /// Total maintenance margin
    /// </summary>
    [JsonProperty("total_maintenance_margin")]
    public decimal TotalMaintenanceMargin { get; set; }

    /// <summary>
    /// Total initial margin rate
    /// </summary>
    [JsonProperty("total_initial_margin_rate")]
    public decimal TotalInitialMarginRate { get; set; }

    /// <summary>
    /// Total maintenance margin rate
    /// </summary>
    [JsonProperty("total_maintenance_margin_rate")]
    public decimal TotalMaintenanceMarginRate { get; set; }

    /// <summary>
    /// Total available margin
    /// </summary>
    [JsonProperty("total_available_margin")]
    public decimal TotalAvailableMargin { get; set; }

    /// <summary>
    /// Total amount of the portfolio margin account
    /// </summary>
    [JsonProperty("portfolio_margin_total")]
    public decimal TotalPortfolioMargin { get; set; }

}
