namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan currency quota
/// </summary>
public record GateMultiCollateralLoanCurrencyQuota
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Currency index price
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Minimum borrowing/collateral limit for the currency
    /// </summary>
    [JsonProperty("min_quota")]
    public decimal MinimumQuota { get; set; }

    /// <summary>
    /// Remaining currency limit
    /// </summary>
    [JsonProperty("left_quota")]
    public decimal LeftQuota { get; set; }

    /// <summary>
    /// Remaining currency limit converted to USDT
    /// </summary>
    [JsonProperty("left_quote_usdt")]
    public decimal LeftQuoteUsdt { get; set; }

    /// <summary>
    /// Remaining fixed-term currency limit
    /// </summary>
    [JsonProperty("left_quota_fixed")]
    public decimal? LeftQuotaFixed { get; set; }

    /// <summary>
    /// Remaining fixed-term currency limit converted to USDT
    /// </summary>
    [JsonProperty("left_quote_usdt_fixed")]
    public decimal? LeftQuoteUsdtFixed { get; set; }
}
