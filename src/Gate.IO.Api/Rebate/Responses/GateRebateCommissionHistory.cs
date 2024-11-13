namespace Gate.IO.Api.Rebate;

/// <summary>
/// GateRebateCommissionHistory
/// </summary>
public class GateRebateCommissionHistory
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
    /// List of comission history
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebateCommissionHistoryRecord> List { get; set; } = [];
}

/// <summary>
/// GateRebateCommissionHistoryRecord
/// </summary>
public class GateRebateCommissionHistoryRecord
{
    /// <summary>
    /// Commission Time. (unix timestamp)
    /// </summary>
    [JsonProperty("commission_time")]
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
    /// Commission Amount
    /// </summary>
    [JsonProperty("commission_amount")]
    public decimal CommissionAmount { get; set; }

    /// <summary>
    /// Commission Asset
    /// </summary>
    [JsonProperty("commission_asset")]
    public string CommissionAsset { get; set; }

    /// <summary>
    /// Source. SPOT - SPOT Rebate, FUTURES - Futures Rebate
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }
}