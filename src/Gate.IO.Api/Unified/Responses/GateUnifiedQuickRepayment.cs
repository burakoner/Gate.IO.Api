namespace Gate.IO.Api.Unified;

/// <summary>
/// Estimated unified account quick repayment details
/// </summary>
public record GateUnifiedQuickRepaymentEstimate
{
    /// <summary>
    /// Liability currencies
    /// </summary>
    [JsonProperty("debt_currencies")]
    public List<GateUnifiedQuickRepaymentDebtItem> DebtCurrencies { get; set; }

    /// <summary>
    /// Currencies available for repayment
    /// </summary>
    [JsonProperty("available_currencies")]
    public List<GateUnifiedQuickRepaymentAvailableItem> AvailableCurrencies { get; set; }
}

/// <summary>
/// Unified account quick repayment liability currency
/// </summary>
public record GateUnifiedQuickRepaymentDebtItem
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Debt quantity
    /// </summary>
    [JsonProperty("debt_amount")]
    public string DebtAmount { get; set; }

    /// <summary>
    /// Estimated USD value
    /// </summary>
    [JsonProperty("estimated_usd")]
    public string EstimatedUsd { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    [JsonProperty("borrowed")]
    public string Borrowed { get; set; }

    /// <summary>
    /// Negative balance
    /// </summary>
    [JsonProperty("neg_balance")]
    public string NegativeBalance { get; set; }
}

/// <summary>
/// Unified account currency available for quick repayment
/// </summary>
public record GateUnifiedQuickRepaymentAvailableItem
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("available")]
    public string Available { get; set; }

    /// <summary>
    /// Estimated USD value
    /// </summary>
    [JsonProperty("estimated_usd")]
    public string EstimatedUsd { get; set; }
}

/// <summary>
/// Unified account quick repayment result
/// </summary>
public record GateUnifiedQuickRepaymentResult
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Repaid currency details
    /// </summary>
    [JsonProperty("repaid_infos")]
    public List<GateUnifiedQuickRepaymentRepaidInfo> RepaidInfos { get; set; }

    /// <summary>
    /// Currencies used for repayment
    /// </summary>
    [JsonProperty("used_infos")]
    public List<GateUnifiedQuickRepaymentUsedInfo> UsedInfos { get; set; }
}

/// <summary>
/// Repaid currency details
/// </summary>
public record GateUnifiedQuickRepaymentRepaidInfo
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Repaid amount
    /// </summary>
    [JsonProperty("repaid")]
    public string Repaid { get; set; }

    /// <summary>
    /// Remaining liability amount
    /// </summary>
    [JsonProperty("left")]
    public string Left { get; set; }
}

/// <summary>
/// Currency used for repayment
/// </summary>
public record GateUnifiedQuickRepaymentUsedInfo
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount converted for repayment
    /// </summary>
    [JsonProperty("used")]
    public string Used { get; set; }
}
