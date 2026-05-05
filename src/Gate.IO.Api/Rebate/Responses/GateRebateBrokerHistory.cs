namespace Gate.IO.Api.Rebate;

/// <summary>
/// Broker rebate commission history
/// </summary>
public record GateRebateBrokerCommissionHistory
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// List of commission history
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebateBrokerCommission> List { get; set; } = [];
}

/// <summary>
/// Broker rebate transaction history
/// </summary>
public record GateRebateBrokerTransactionHistory
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// List of transaction history
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebateBrokerTransaction> List { get; set; } = [];
}

/// <summary>
/// Broker rebate commission record
/// </summary>
public record GateRebateBrokerCommission
{
    [JsonProperty("commission_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CommissionTime { get; set; }

    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("fee_asset")]
    public string FeeAsset { get; set; }

    [JsonProperty("rebate_fee")]
    public decimal RebateFee { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("sub_broker_info")]
    public GateRebateSubBrokerInfo SubBrokerInfo { get; set; }

    [JsonProperty("alpha_contract_addr")]
    public string AlphaContractAddress { get; set; }
}

/// <summary>
/// Broker rebate transaction record
/// </summary>
public record GateRebateBrokerTransaction
{
    [JsonProperty("transaction_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime TransactionTime { get; set; }

    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("fee_asset")]
    public string FeeAsset { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }

    [JsonProperty("sub_broker_info")]
    public GateRebateSubBrokerInfo SubBrokerInfo { get; set; }

    [JsonProperty("alpha_contract_addr")]
    public string AlphaContractAddress { get; set; }
}

/// <summary>
/// Sub-broker information
/// </summary>
public record GateRebateSubBrokerInfo
{
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("original_commission_rate")]
    public decimal OriginalCommissionRate { get; set; }

    [JsonProperty("relative_commission_rate")]
    public decimal RelativeCommissionRate { get; set; }

    [JsonProperty("commission_rate")]
    public decimal CommissionRate { get; set; }
}
