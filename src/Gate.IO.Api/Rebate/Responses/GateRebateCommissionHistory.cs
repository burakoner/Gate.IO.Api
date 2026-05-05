namespace Gate.IO.Api.Rebate;

/// <summary>
/// Rebate commission history
/// </summary>
public record GateRebateCommissionHistory
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
    /// List of commission history
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebateCommissionHistoryRecord> List { get; set; } = [];
}

/// <summary>
/// Rebate commission history record
/// </summary>
public record GateRebateCommissionHistoryRecord
{
    /// <summary>
    /// Commission time
    /// </summary>
    [JsonProperty("commission_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CommissionTime { get; set; }

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
    /// Commission amount
    /// </summary>
    [JsonProperty("commission_amount")]
    public decimal CommissionAmount { get; set; }

    /// <summary>
    /// Commission asset
    /// </summary>
    [JsonProperty("commission_asset")]
    public string CommissionAsset { get; set; }

    /// <summary>
    /// Commission source
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// Commission time compatibility alias
    /// </summary>
    [JsonIgnore]
    public DateTime TransactionTime
    {
        get => CommissionTime;
        set => CommissionTime = value;
    }
}
