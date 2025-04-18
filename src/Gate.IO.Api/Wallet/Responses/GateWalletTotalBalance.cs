namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Total Balance
/// </summary>
public record GateWalletTotalBalance
{
    /// <summary>
    /// Total balances calculated with specified currency unit
    /// </summary>
    [JsonProperty("total")]
    public GateWalletTotalBalanceData Total { get; set; }

    /// <summary>
    /// Total balances in different accounts
    /// </summary>
    [JsonProperty("details")]
    public GateWalletTotalBalanceDetails Details { get; set; }
}

/// <summary>
/// Gate.IO Wallet Total Balance Item
/// </summary>
public record GateWalletTotalBalanceData
{
    /// <summary>
    /// Account total balance amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Unrealised_pnl, this field will only appear in futures, options, delivery, and total accounts
    /// </summary>
    [JsonProperty("unrealised_pnl")]
    public decimal? UnrealisedPnl { get; set; }

    /// <summary>
    /// Borrowed，this field will only appear in margin and cross_margin accounts
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal? Borrowed { get; set; }
}

/// <summary>
/// Gate.IO Wallet Total Balance Details
/// </summary>
public record GateWalletTotalBalanceDetails
{
    /// <summary>
    /// Cross margin account
    /// </summary>
    [JsonProperty("cross_margin")]
    public GateWalletTotalBalanceData CrossMargin { get; set; }

    /// <summary>
    /// Spot account
    /// </summary>
    [JsonProperty("spot")]
    public GateWalletTotalBalanceData Spot { get; set; }

    /// <summary>
    /// Finance account
    /// </summary>
    [JsonProperty("finance")]
    public GateWalletTotalBalanceData Finance { get; set; }

    /// <summary>
    /// Margin account
    /// </summary>
    [JsonProperty("margin")]
    public GateWalletTotalBalanceData Margin { get; set; }

    /// <summary>
    /// Quant account
    /// </summary>
    [JsonProperty("quant")]
    public GateWalletTotalBalanceData Quant { get; set; }

    /// <summary>
    /// Futures account
    /// </summary>
    [JsonProperty("futures")]
    public GateWalletTotalBalanceData Futures { get; set; }

    /// <summary>
    /// Delivery account
    /// </summary>
    [JsonProperty("delivery")]
    public GateWalletTotalBalanceData Delivery { get; set; }

    /// <summary>
    /// Warrant account
    /// </summary>
    [JsonProperty("warrant")]
    public GateWalletTotalBalanceData Warrant { get; set; }

    /// <summary>
    /// CBBC account
    /// </summary>
    [JsonProperty("cbbc")]
    public GateWalletTotalBalanceData CBBC { get; set; }
}
