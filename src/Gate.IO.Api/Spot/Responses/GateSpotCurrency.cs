namespace Gate.IO.Api.Spot;

/// <summary>
/// Currency
/// </summary>
public record GateSpotCurrency
{
    /// <summary>
    /// Currency symbol
    /// </summary>
    [JsonProperty("currency")]
    public string Symbol { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Whether currency is de-listed
    /// </summary>
    [JsonProperty("delisted")]
    public bool Delisted { get; set; }

    /// <summary>
    /// Whether currency&#39;s withdrawal is disabled
    /// </summary>
    [JsonProperty("withdraw_disabled")]
    public bool WithdrawDisabled { get; set; }

    /// <summary>
    /// Whether currency&#39;s withdrawal is delayed
    /// </summary>
    [JsonProperty("withdraw_delayed")]
    public bool WithdrawDelayed { get; set; }

    /// <summary>
    /// Whether currency&#39;s deposit is disabled
    /// </summary>
    [JsonProperty("deposit_disabled")]
    public bool DepositDisabled { get; set; }

    /// <summary>
    /// Whether currency&#39;s trading is disabled
    /// </summary>
    [JsonProperty("trade_disabled")]
    public bool TradeDisabled { get; set; }

    /// <summary>
    /// Fixed fee rate. Only for fixed rate currencies, not valid for normal currencies
    /// </summary>
    [JsonProperty("fixed_rate")]
    public decimal? FixedFeeRate { get; set; }

    /// <summary>
    /// Chain of currency
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// All links corresponding to coins
    /// </summary>
    [JsonProperty("chains")]
    public List<GateSpotCurrencyChain> Chains { get; set; }
}

/// <summary>
/// GateSpotCurrencyChain
/// </summary>
public record GateSpotCurrencyChain
{
    /// <summary>
    /// Chain name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// token address
    /// </summary>
    [JsonProperty("addr")]
    public string Address { get; set; }

    /// <summary>
    /// Whether currency&#39;s withdrawal is disabled
    /// </summary>
    [JsonProperty("withdraw_disabled")]
    public bool WithdrawDisabled { get; set; }

    /// <summary>
    /// Whether currency&#39;s withdrawal is delayed
    /// </summary>
    [JsonProperty("withdraw_delayed")]
    public bool WithdrawDelayed { get; set; }

    /// <summary>
    /// Whether currency&#39;s deposit is disabled
    /// </summary>
    [JsonProperty("deposit_disabled")]
    public bool DepositDisabled { get; set; }
}
