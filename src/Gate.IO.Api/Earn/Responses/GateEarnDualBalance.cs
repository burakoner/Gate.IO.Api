namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual-currency earning assets
/// </summary>
public record GateEarnDualBalance
{
    /// <summary>
    /// User assets in USDT equivalent
    /// </summary>
    [JsonProperty("user_asset_usdt")]
    public decimal UserAssetUsdt { get; set; }

    /// <summary>
    /// User assets in BTC equivalent
    /// </summary>
    [JsonProperty("user_asset_btc")]
    public decimal UserAssetBtc { get; set; }

    /// <summary>
    /// Total user interest in USDT equivalent
    /// </summary>
    [JsonProperty("user_total_interest_usdt")]
    public decimal UserTotalInterestUsdt { get; set; }

    /// <summary>
    /// Total user interest in BTC equivalent
    /// </summary>
    [JsonProperty("user_total_interest_btc")]
    public decimal UserTotalInterestBtc { get; set; }
}
