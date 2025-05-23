namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Small Balance History
/// </summary>
public record GateWalletSmallBalanceHistory
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Exchange time (in seconds)
    /// </summary>
    [JsonProperty("create_time")]
    public string CreateTime { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// GT Amount
    /// </summary>
    [JsonProperty("gt_amount")]
    public decimal GTAmount { get; set; }
}
