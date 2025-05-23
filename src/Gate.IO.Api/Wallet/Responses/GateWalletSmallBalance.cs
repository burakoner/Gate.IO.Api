namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Small Balance
/// </summary>
public record GateWalletSmallBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("available_balance")]
    public decimal AvailableBalance { get; set; }

    /// <summary>
    /// Estimated as BTC
    /// </summary>
    [JsonProperty("estimated_as_btc")]
    public decimal EstimatedAsBTC { get; set; }

    /// <summary>
    /// Estimated conversion to GT
    /// </summary>
    [JsonProperty("convertible_to_gt")]
    public decimal EstimatedAsGT { get; set; }
}
