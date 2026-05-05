namespace Gate.IO.Api.Rebate;

/// <summary>
/// Rebate transaction history
/// </summary>
public record GateRebateTransactionHistory
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// List of transaction history
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebateTransactionHistoryRecord> List { get; set; } = [];
}

/// <summary>
/// Rebate transaction history record
/// </summary>
public record GateRebateTransactionHistoryRecord
{
    /// <summary>
    /// Transaction time
    /// </summary>
    [JsonProperty("transaction_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime TransactionTime { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("fee_asset")]
    public string FeeAsset { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Transaction amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Transaction amount currency
    /// </summary>
    [JsonProperty("amount_asset")]
    public string AmountAsset { get; set; }

    /// <summary>
    /// Commission source
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// Commission amount compatibility alias
    /// </summary>
    [JsonIgnore]
    public decimal Commission
    {
        get => Amount;
        set => Amount = value;
    }

    /// <summary>
    /// Commission asset compatibility alias
    /// </summary>
    [JsonIgnore]
    public string CommissionAsset
    {
        get => AmountAsset;
        set => AmountAsset = value;
    }
}
