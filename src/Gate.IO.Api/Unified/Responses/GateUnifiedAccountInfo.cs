namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account info. Values are server-calculated and mode-dependent; all schema properties are optional.
/// Legacy nonnullable defaults do not prove zero liability, an unlocked account or available funds.
/// https://www.gate.com/docs/developers/apiv4/en/unified/#get-unified-account-information
/// </summary>
public record GateUnifiedAccountInfo
{
    /// <summary>
    /// Unified account mode
    /// </summary>
    [JsonProperty("mode"), JsonConverter(typeof(MapConverter))]
    public GateUnifiedAccountMode Mode { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    [JsonProperty("user_id")]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// Last refresh time. The schema does not state a unit; the existing timestamp converter is retained.
    /// </summary>
    [JsonProperty("refresh_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime RefreshTime { get; set; }

    /// <summary>
    /// Locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Balances
    /// </summary>
    [JsonProperty("balances")]
    public Dictionary<string, GateIoUnifiedAccountBalance> Balances { get; set; } = [];

    /// <summary>
    /// Deprecated total assets converted to USD. Prefer UnifiedAccountTotal; retained for compatibility.
    /// </summary>
    [JsonProperty("total")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Total { get; set; }

    /// <summary>
    /// Borrowed value in USD, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("borrowed")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Cross initial margin, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_initial_margin")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalInitialMargin { get; set; }

    /// <summary>
    /// Cross margin balance, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_margin_balance")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalMarginBalance { get; set; }

    /// <summary>
    /// Cross maintenance margin, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_maintenance_margin")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalMaintenanceMargin { get; set; }

    /// <summary>
    /// Cross initial margin rate, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_initial_margin_rate")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalInitialMarginRate { get; set; }

    /// <summary>
    /// Cross maintenance margin rate, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_maintenance_margin_rate")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalMaintenanceMarginRate { get; set; }

    /// <summary>
    /// Available cross margin, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_available_margin")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalAvailableMargin { get; set; }

    /// <summary>
    /// Total assets: cross and isolated in single-/multi-currency mode; cross only in portfolio mode.
    /// </summary>
    [JsonProperty("unified_account_total")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal UnifiedAccountTotal { get; set; }

    /// <summary>
    /// Total cross liabilities, effective in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("unified_account_total_liab")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal UnifiedAccountTotalLiabilities { get; set; }

    /// <summary>
    /// Total equity: cross and isolated in single-/multi-currency mode; cross only in portfolio mode.
    /// </summary>
    [JsonProperty("unified_account_total_equity")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal UnifiedAccountTotalEquity { get; set; }

    /// <summary>
    /// Deprecated account leverage in multi-currency/portfolio mode. Query /unified/leverage/user_currency_setting.
    /// </summary>
    [JsonProperty("leverage")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Spot pending-order loss, in USDT; effective in multi-currency/portfolio mode only.
    /// </summary>
    [JsonProperty("spot_order_loss")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalOrderLoss { get; set; }

    /// <summary>
    /// Option pending-order loss, in USDT; effective in portfolio mode only.
    /// </summary>
    [JsonProperty("options_order_loss")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal OptionsOrderLoss { get; set; }

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
    /// Whether all currencies are used as margin: true - all currencies as margin, false - no
    /// </summary>
    [JsonProperty("is_all_collateral")]
    public bool? IsAllCollateral { get; set; }
}

/// <summary>
/// Unified account balance
/// </summary>
public record GateIoUnifiedAccountBalance
{
    /// <summary>
    /// Cross available quantity after isolated occupation and frozen funds, in all three margin modes.
    /// </summary>
    [JsonProperty("available")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Available { get; set; }

    /// <summary>
    /// Frozen quantity
    /// </summary>
    [JsonProperty("freeze")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Frozen { get; set; }

    /// <summary>
    /// Borrowed quantity in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("borrowed")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Negative-balance borrowing in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("negative_liab")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal NegativeLiabilities { get; set; }

    /// <summary>
    /// Deprecated contract-opening borrowing currency, scheduled for removal; retained for compatibility.
    /// </summary>
    [JsonProperty("futures_pos_liab")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal FuturesPositionLiabilities { get; set; }

    /// <summary>
    /// Cross currency equity in single-currency/multi-currency/portfolio mode.
    /// </summary>
    [JsonProperty("equity")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Equity { get; set; }

    /// <summary>
    /// Deprecated total frozen, scheduled for removal; retained for compatibility.
    /// </summary>
    [JsonProperty("total_freeze")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalFrozen { get; set; }

    /// <summary>
    /// Total borrowed quantity in multi-currency/portfolio mode; zero in single-currency mode.
    /// </summary>
    [JsonProperty("total_liab")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TotalLiabilities { get; set; }

    /// <summary>
    /// Spot hedging utilization in portfolio mode; zero in other margin modes.
    /// </summary>
    [JsonProperty("spot_in_use")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal SpotInUse { get; set; }

    /// <summary>
    /// Uniloan financial management amount, effective when turned on as a unified account margin switch
    /// </summary>
    [JsonProperty("funding")]
    public string Funding { get; set; }

    /// <summary>
    /// Funding version
    /// </summary>
    [JsonProperty("funding_version")]
    public string FundingVersion { get; set; }

    /// <summary>
    /// Cross balance in single-/multi-currency mode; zero in portfolio mode.
    /// </summary>
    [JsonProperty("cross_balance")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? CrossMarginBalance { get; set; }

    /// <summary>
    /// Futures isolated balance in single-/multi-currency mode; zero in portfolio mode.
    /// </summary>
    [JsonProperty("iso_balance")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? IsolatedMarginBalance { get; set; }

    /// <summary>
    /// Cross initial margin: effective for USDT in single-currency mode only; zero in other margin modes.
    /// </summary>
    [JsonProperty("im")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? InitialMargin { get; set; }

    /// <summary>
    /// Cross maintenance margin: effective for USDT in single-currency mode only; zero in other margin modes.
    /// </summary>
    [JsonProperty("mm")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? MaintenanceMargin { get; set; }

    /// <summary>
    /// Cross initial margin rate: effective for USDT in single-currency mode only; zero in other margin modes.
    /// </summary>
    [JsonProperty("imr")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? InitialMarginRate { get; set; }

    /// <summary>
    /// Cross maintenance margin rate: effective for USDT in single-currency mode only; zero in other margin modes.
    /// </summary>
    [JsonProperty("mmr")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? MaintenanceMarginRate { get; set; }

    /// <summary>
    /// Cross margin balance: effective for USDT in single-currency mode only; zero in other margin modes.
    /// </summary>
    [JsonProperty("margin_balance")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? MarginBalance { get; set; }

    /// <summary>
    /// Cross available margin: effective for USDT in single-currency mode only; zero in other margin modes.
    /// </summary>
    [JsonProperty("available_margin")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? AvailableMargin { get; set; }

    /// <summary>
    /// Currency enabled as margin: true - Enabled, false - Disabled
    /// </summary>
    [JsonProperty("enabled_collateral")]
    public bool? IsCollateralEnabled { get; set; }

    /// <summary>
    /// Balance version number
    /// </summary>
    [JsonProperty("balance_version")]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? BalanceVersion { get; set; }
}
