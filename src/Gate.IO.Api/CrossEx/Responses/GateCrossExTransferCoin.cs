namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx supported transfer currency
/// </summary>
public record GateCrossExTransferCoin
{
    /// <summary>
    /// Gets or sets the Coin.
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Gets or sets the Minimum Transfer Amount.
    /// </summary>
    [JsonProperty("min_trans_amount")]
    public decimal MinimumTransferAmount { get; set; }

    /// <summary>
    /// Gets or sets the Estimated Fee.
    /// </summary>
    [JsonProperty("est_fee")]
    public decimal EstimatedFee { get; set; }

    /// <summary>
    /// Gets or sets the Precision.
    /// </summary>
    [JsonProperty("precision")]
    public int Precision { get; set; }

    /// <summary>
    /// Gets or sets the Is Disabled.
    /// </summary>
    [JsonProperty("is_disabled")]
    public int IsDisabled { get; set; }
}
