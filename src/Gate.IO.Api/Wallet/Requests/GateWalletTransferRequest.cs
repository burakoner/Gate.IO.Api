namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet trading account transfer request
/// </summary>
public record GateWalletTransferRequest
{
    /// <summary>
    /// Transfer currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Account to transfer from
    /// </summary>
    [JsonProperty("from")]
    public GateWalletAccountType From { get; set; }

    /// <summary>
    /// Account to transfer to
    /// </summary>
    [JsonProperty("to")]
    public GateWalletAccountType To { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Margin trading pair
    /// </summary>
    [JsonProperty("currency_pair", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Contract settlement currency
    /// </summary>
    [JsonProperty("settle", NullValueHandling = NullValueHandling.Ignore)]
    public string Settle { get; set; }
}
