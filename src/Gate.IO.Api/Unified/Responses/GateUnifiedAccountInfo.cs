namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account info
/// </summary>
public record GateUnifiedAccountInfo
{
    /// <summary>
    /// User id
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Total value in USD
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Borrowed value in USD
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Total initial margin
    /// </summary>
    [JsonProperty("total_initial_margin")]
    public decimal TotalInitialMargin { get; set; }

    /// <summary>
    /// Total margin balance
    /// </summary>
    [JsonProperty("total_margin_balance")]
    public decimal TotalMarginBalance { get; set; }

    /// <summary>
    /// Total maintenance margin
    /// </summary>
    [JsonProperty("total_maintenance_margin")]
    public decimal TotalMaintenanceMargin { get; set; }

    /// <summary>
    /// Total initial margin rate
    /// </summary>
    [JsonProperty("total_initial_margin_rate")]
    public decimal TotalInitialMarginRate { get; set; }

    /// <summary>
    /// Total maintenance margin rate
    /// </summary>
    [JsonProperty("total_maintenance_margin_rate")]
    public decimal TotalMaintenanceMarginRate { get; set; }

    /// <summary>
    /// Total available margin
    /// </summary>
    [JsonProperty("total_available_margin")]
    public decimal TotalAvailableMargin { get; set; }

    /// <summary>
    /// Unified account total
    /// </summary>
    [JsonProperty("unified_account_total")]
    public decimal UnifiedAccountTotal { get; set; }

    /// <summary>
    /// Unified account total liabilities
    /// </summary>
    [JsonProperty("unified_account_total_liab")]
    public decimal UnifiedAccountTotalLiabilities { get; set; }

    /// <summary>
    /// Unified account total equity
    /// </summary>
    [JsonProperty("unified_account_total_equity")]
    public decimal UnifiedAccountTotalEquity { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Total order loss, in USDT
    /// </summary>
    [JsonProperty("spot_order_loss")]
    public decimal TotalOrderLoss { get; set; }

    /// <summary>
    /// Spot hedging status
    /// </summary>
    [JsonProperty("spot_hedge")]
    public bool SpotHedge { get; set; }

    /// <summary>
    /// Whether to use funds as margin
    /// </summary>
    [JsonProperty("use_funding")]
    public bool? UseFunding { get; set; }

    /// <summary>
    /// Balances
    /// </summary>
    [JsonProperty("balances")]
    public Dictionary<string, GateIoUnifiedAccountBalance> Balances { get; set; } = [];
}

/// <summary>
/// Unified account balance
/// </summary>
public record GateIoUnifiedAccountBalance
{
    /// <summary>
    /// Available quantity
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Frozen quantity
    /// </summary>
    [JsonProperty("freeze")]
    public decimal Frozen { get; set; }

    /// <summary>
    /// Borrowed quantity
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Negative liabilities
    /// </summary>
    [JsonProperty("negative_liab")]
    public decimal NegativeLiabilities { get; set; }

    /// <summary>
    /// Borrowing to open futures positions
    /// </summary>
    [JsonProperty("futures_pos_liab")]
    public decimal FuturesPositionLiabilities { get; set; }

    /// <summary>
    /// Equity
    /// </summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }

    /// <summary>
    /// Total frozen
    /// </summary>
    [JsonProperty("total_freeze")]
    public decimal TotalFrozen { get; set; }

    /// <summary>
    /// Total liabilities
    /// </summary>
    [JsonProperty("total_liab")]
    public decimal TotalLiabilities { get; set; }

    /// <summary>
    /// Spot hedging utilization
    /// </summary>
    [JsonProperty("spot_in_use")]
    public decimal SpotInUse { get; set; }

    /// <summary>
    /// Cross margin balance
    /// </summary>
    [JsonProperty("cross_balance")]
    public decimal? CrossMarginBalance { get; set; }

    /// <summary>
    /// Isolated margin balance
    /// </summary>
    [JsonProperty("iso_balance")]
    public decimal? IsolatedMarginBalance { get; set; }

    /// <summary>
    /// Initial margin
    /// </summary>
    [JsonProperty("im")]
    public decimal? InitialMargin { get; set; }

    /// <summary>
    /// Maintenance margin
    /// </summary>
    [JsonProperty("mm")]
    public decimal? MaintenanceMargin { get; set; }

    /// <summary>
    /// Initial margin rate
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRate { get; set; }

    /// <summary>
    /// Maintenance margin rate
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRate { get; set; }

    /// <summary>
    /// Margin balance
    /// </summary>
    [JsonProperty("margin_balance")]
    public decimal? MarginBalance { get; set; }

    /// <summary>
    /// Available margin
    /// </summary>
    [JsonProperty("available_margin")]
    public decimal? AvailableMargin { get; set; }
}
