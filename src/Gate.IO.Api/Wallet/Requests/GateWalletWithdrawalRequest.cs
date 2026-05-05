namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet withdrawal request
/// </summary>
public record GateWalletWithdrawalRequest
{
    /// <summary>
    /// User-defined order number for withdrawal
    /// </summary>
    [JsonProperty("withdraw_order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string WithdrawalOrderId { get; set; }

    /// <summary>
    /// Token amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Withdrawal address. Required for withdrawals
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Additional remarks with regards to the withdrawal
    /// </summary>
    [JsonProperty("memo", NullValueHandling = NullValueHandling.Ignore)]
    public string Memo { get; set; }

    /// <summary>
    /// The withdrawal record id starts with w, such as: w1879219868. When withdraw_id is not empty, the value queries this withdrawal record and no longer queries according to time
    /// </summary>
    [JsonProperty("withdraw_id", NullValueHandling = NullValueHandling.Ignore)]
    public string WithdrawalId { get; set; }

    /// <summary>
    /// The currency type of withdrawal record is empty by default. It supports users to query the withdrawal records in the main and innovation areas on demand.
    /// </summary>
    [JsonProperty("asset_class", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateWalletAssetClass? AssetClass { get; set; }

    /// <summary>
    /// Name of the chain used in withdrawals
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }
}
