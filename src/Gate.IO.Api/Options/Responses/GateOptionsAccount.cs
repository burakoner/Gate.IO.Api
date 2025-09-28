namespace Gate.IO.Api.Options;

/// <summary>
/// Gate Options Account Information
/// </summary>
public record GateOptionsAccount
{
    /// <summary>
    /// User Id
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Account Balance
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Position value, long position value is positive, short position value is negative
    /// </summary>
    [JsonProperty("position_value")]
    public decimal PositionValue { get; set; }

    /// <summary>
    /// Account equity, the sum of account balance and position value
    /// </summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }

    /// <summary>
    /// If the account is allowed to short
    /// </summary>
    [JsonProperty("short_enabled")]
    public bool ShortEnabled { get; set; }

    /// <summary>
    /// Whether to enable MMP
    /// </summary>
    [JsonProperty("mmp_enabled")]
    public bool MMPEnabled { get; set; }

    /// <summary>
    /// Whether to trigger position liquidation
    /// </summary>
    [JsonProperty("liq_triggered")]
    public bool LiquidationTriggered { get; set; }

    /// <summary>
    /// Margin Mode
    /// </summary>
    [JsonProperty("margin_mode"), JsonConverter(typeof(MapConverter))]
    public GateOptionsMarginMode MarginMode { get; set; }

    /// <summary>
    /// Unrealized PNL
    /// </summary>
    [JsonProperty("unrealised_pnl")]
    public decimal UnrealisedPnl { get; set; }

    /// <summary>
    /// Initial position margin
    /// </summary>
    [JsonProperty("init_margin")]
    public decimal InitialMargin { get; set; }

    /// <summary>
    /// Position maintenance margin
    /// </summary>
    [JsonProperty("maint_margin")]
    public decimal MaintenanceMargin { get; set; }

    /// <summary>
    /// Order margin of unfinished orders
    /// </summary>
    [JsonProperty("order_margin")]
    public decimal OrderMargin { get; set; }

    /// <summary>
    /// Margin for outstanding sell orders
    /// </summary>
    [JsonProperty("ask_order_margin")]
    public decimal AskOrderMargin { get; set; }

    /// <summary>
    /// Margin for outstanding buy orders
    /// </summary>
    [JsonProperty("bid_order_margin")]
    public decimal BidOrderMargin { get; set; }

    /// <summary>
    /// Available balance to transfer out or trade
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Point card amount
    /// </summary>
    [JsonProperty("point")]
    public decimal Point { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Maximum number of outstanding orders
    /// </summary>
    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }

    /// <summary>
    /// Notional value upper limit, including the nominal value of positions and outstanding orders
    /// </summary>
    [JsonProperty("position_notional_limit")]
    public long PositionNotionalLimit { get; set; }
}