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
    /// <summary>
    /// Gets or sets the Commission Time.
    /// </summary>
    [JsonProperty("commission_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CommissionTime { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Group Name.
    /// </summary>
    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Gets or sets the Fee Asset.
    /// </summary>
    [JsonProperty("fee_asset")]
    public string FeeAsset { get; set; }

    /// <summary>
    /// Gets or sets the Rebate Fee.
    /// </summary>
    [JsonProperty("rebate_fee")]
    public decimal RebateFee { get; set; }

    /// <summary>
    /// Gets or sets the Source.
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Sub Broker Info.
    /// </summary>
    [JsonProperty("sub_broker_info")]
    public GateRebateSubBrokerInfo SubBrokerInfo { get; set; }

    /// <summary>
    /// Gets or sets the Alpha Contract Address.
    /// </summary>
    [JsonProperty("alpha_contract_addr")]
    public string AlphaContractAddress { get; set; }
}

/// <summary>
/// Broker rebate transaction record
/// </summary>
public record GateRebateBrokerTransaction
{
    /// <summary>
    /// Gets or sets the Transaction Time.
    /// </summary>
    [JsonProperty("transaction_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime TransactionTime { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Group Name.
    /// </summary>
    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the Fee Asset.
    /// </summary>
    [JsonProperty("fee_asset")]
    public string FeeAsset { get; set; }

    /// <summary>
    /// Gets or sets the Source.
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the Sub Broker Info.
    /// </summary>
    [JsonProperty("sub_broker_info")]
    public GateRebateSubBrokerInfo SubBrokerInfo { get; set; }

    /// <summary>
    /// Gets or sets the Alpha Contract Address.
    /// </summary>
    [JsonProperty("alpha_contract_addr")]
    public string AlphaContractAddress { get; set; }
}

/// <summary>
/// Sub-broker information
/// </summary>
public record GateRebateSubBrokerInfo
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Original Commission Rate.
    /// </summary>
    [JsonProperty("original_commission_rate")]
    public decimal OriginalCommissionRate { get; set; }

    /// <summary>
    /// Gets or sets the Relative Commission Rate.
    /// </summary>
    [JsonProperty("relative_commission_rate")]
    public decimal RelativeCommissionRate { get; set; }

    /// <summary>
    /// Gets or sets the Commission Rate.
    /// </summary>
    [JsonProperty("commission_rate")]
    public decimal CommissionRate { get; set; }
}
