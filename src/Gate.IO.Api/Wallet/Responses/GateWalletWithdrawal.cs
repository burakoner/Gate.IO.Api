namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Withdrawal
/// </summary>
public record GateWalletWithdrawal
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Currency Chinese name
    /// </summary>
    [JsonProperty("name_cn")]
    public string ChineseName { get; set; }

    /// <summary>
    /// Deposits fee
    /// </summary>
    [JsonProperty("deposit")]
    public decimal Deposit { get; set; }

    /// <summary>
    /// Withdrawal fee rate percentage
    /// </summary>
    [JsonProperty("withdraw_percent")]
    public decimal WithdrawalPercent { get; set; }

    /// <summary>
    /// Fixed withdrawal fee
    /// </summary>
    [JsonProperty("withdraw_fix")]
    public decimal WithdrawalFix { get; set; }

    /// <summary>
    /// Daily allowed withdrawal amount
    /// </summary>
    [JsonProperty("withdraw_day_limit")]
    public decimal WithdrawalDayLimit { get; set; }

    /// <summary>
    /// Minimum withdrawal amount
    /// </summary>
    [JsonProperty("withdraw_amount_mini")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// Daily withdrawal amount left
    /// </summary>
    [JsonProperty("withdraw_day_limit_remain")]
    public decimal WithdrawalDayLimitRemain { get; set; }

    /// <summary>
    /// Maximum amount for each withdrawal
    /// </summary>
    [JsonProperty("withdraw_eachtime_limit")]
    public decimal WithdrawalEachTimeLimit { get; set; }

    /// <summary>
    /// Fixed withdrawal fee on multiple chains
    /// </summary>
    [JsonProperty("withdraw_fix_on_chains")]
    public Dictionary<string, string> WithdrawFixOnChains { get; set; }

    /// <summary>
    /// Percentage withdrawal fee on multiple chains
    /// </summary>
    [JsonProperty("withdraw_percent_on_chains")]
    public Dictionary<string, string> WithdrawPercentOnChains { get; set; }
}
