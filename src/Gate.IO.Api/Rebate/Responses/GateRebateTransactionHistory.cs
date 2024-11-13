namespace Gate.IO.Api.Rebate;

/// <summary>
/// GateRebateTransactionHistory
/// </summary>
public class GateRebateTransactionHistory
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
/// GateRebateTransactionHistoryRecord
/// </summary>
public class GateRebateTransactionHistoryRecord
{
    /// <summary>
    /// Transaction Time. (unix timestamp)
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
    /// Commission Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Commission  { get; set; }

    /// <summary>
    /// Commission Asset
    /// </summary>
    [JsonProperty("amount_asset")]
    public string CommissionAsset { get; set; }

    /// <summary>
    /// Source. SPOT - SPOT Rebate, FUTURES - Futures Rebate
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

}