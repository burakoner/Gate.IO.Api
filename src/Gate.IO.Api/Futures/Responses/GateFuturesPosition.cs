namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate Futures Position
/// </summary>
public record GateFuturesPosition
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Position size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Position leverage. 0 means cross margin; positive number means isolated margin
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Position risk limit
    /// </summary>
    [JsonProperty("risk_limit")]
    public decimal RiskLimit { get; set; }

    /// <summary>
    /// Maximum leverage under current risk limit
    /// </summary>
    [JsonProperty("leverage_max")]
    public decimal LeverageMax { get; set; }

    /// <summary>
    /// Maintenance rate under current risk limit
    /// </summary>
    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    /// <summary>
    /// Position value calculated in settlement currency
    /// </summary>
    [JsonProperty("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Position margin
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Entry price
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonProperty("liq_price")]
    public decimal LiquidationPrice { get; set; }

    /// <summary>
    /// Current mark price
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// The initial margin occupied by the position, applicable to the portfolio margin account
    /// </summary>
    [JsonProperty("initial_margin")]
    public decimal InitialMargin { get; set; }

    /// <summary>
    /// Maintenance margin required for the position, applicable to portfolio margin account
    /// </summary>
    [JsonProperty("maintenance_margin")]
    public decimal MaintenanceMargin { get; set; }

    /// <summary>
    /// Unrealized PNL
    /// </summary>
    [JsonProperty("unrealised_pnl")]
    public decimal UnrealisedPnl { get; set; }

    /// <summary>
    /// Realized PNL
    /// </summary>
    [JsonProperty("realised_pnl")]
    public decimal RealisedPnl { get; set; }

    /// <summary>
    /// Realized PNL - Position P/L
    /// </summary>
    [JsonProperty("pnl_pnl")]
    public decimal PnlPnl { get; set; }

    /// <summary>
    /// Realized PNL - Funding Fees
    /// </summary>
    [JsonProperty("pnl_fund")]
    public decimal PnlFund { get; set; }

    /// <summary>
    /// Realized PNL - Transaction Fees
    /// </summary>
    [JsonProperty("pnl_fee")]
    public decimal PnlFee { get; set; }

    /// <summary>
    /// History realized PNL
    /// </summary>
    [JsonProperty("history_pnl")]
    public decimal HistoryPnl { get; set; }

    /// <summary>
    /// PNL of last position close
    /// </summary>
    [JsonProperty("last_close_pnl")]
    public decimal LastClosePnl { get; set; }

    /// <summary>
    /// Realized POINT PNL
    /// </summary>
    [JsonProperty("realised_point")]
    public decimal RealisedPoint { get; set; }

    /// <summary>
    /// History realized POINT PNL
    /// </summary>
    [JsonProperty("history_point")]
    public decimal HistoryPoint { get; set; }

    /// <summary>
    /// ADL ranking, ranging from 1 to 5
    /// </summary>
    [JsonProperty("adl_ranking")]
    public int AdlRanking { get; set; }

    /// <summary>
    /// Current open orders
    /// </summary>
    [JsonProperty("pending_orders")]
    public int PendingOrders { get; set; }

    /// <summary>
    /// Gets or Sets CloseOrder
    /// </summary>
    [JsonProperty("close_order", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesPositionCloseOrder CloseOrder { get; set; }
    
    /// <summary>
    /// Position mode, including:  - &#x60;single&#x60;: dual mode is not enabled- &#x60;dual_long&#x60;: long position in dual mode- &#x60;dual_short&#x60;: short position in dual mode
    /// </summary>
    [JsonProperty("mode")]
    public GateFuturesPositionMode Mode { get; set; }

    /// <summary>
    /// Cross margin leverage(valid only when &#x60;leverage&#x60; is 0)
    /// </summary>
    [JsonProperty("cross_leverage_limit")]
    public decimal CrossLeverageLimit { get; set; }
 
    /// <summary>
    /// Last update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Update ID. The value increments by 1 each time the position is updated
    /// </summary>
    [JsonProperty("update_id")]
    public long UpdateId { get; set; }

    /// <summary>
    /// Cumulative long size
    /// </summary>
    [JsonProperty("trade_long_size")]
    public long TradeLongSize { get; set; }

    /// <summary>
    /// Cumulative long size*price
    /// </summary>
    [JsonProperty("trade_long_xprice")]
    public decimal TradeLongXPrice { get; set; }

    /// <summary>
    /// Cumulative short size
    /// </summary>
    [JsonProperty("trade_short_size")]
    public long TradeShortSize { get; set; }

    /// <summary>
    /// Cumulative short size*price
    /// </summary>
    [JsonProperty("trade_short_xprice")]
    public decimal TradeShortXPrice { get; set; }

    /// <summary>
    /// Maximum position
    /// </summary>
    [JsonProperty("trade_max_size")]
    public decimal TradeMaxSize { get; set; }

    /// <summary>
    /// First Open Time
    /// </summary>
    [JsonProperty("open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime OpenTime { get; set; }

    /// <summary>
    /// Risk limit table ID
    /// </summary>
    [JsonProperty("risk_limit_table")]
    public string RiskLimitTable { get; set; }

    /// <summary>
    /// Average maintenance margin rate
    /// </summary>
    [JsonProperty("average_maintenance_rate")]
    public decimal? AverageMaintenanceRate { get; set; }

    /// <summary>
    /// Experience Coupon Position Size
    /// </summary>
    [JsonProperty("voucher_size")]
    public decimal? VoucherSize { get; set; }

    /// <summary>
    /// Experience Coupon Position Margin
    /// </summary>
    [JsonProperty("voucher_margin")]
    public decimal? VoucherMargin { get; set; }

    /// <summary>
    /// Experience Coupon ID
    /// </summary>
    [JsonProperty("voucher_id")]
    public long? VoucherId { get; set; }

    /// <summary>
    /// 分仓仓位id
    /// </summary>
    [JsonProperty("pid")]
    public long? PID { get; set; }
}

/// <summary>
/// GateFuturesPositionCloseOrder
/// </summary>
public record GateFuturesPositionCloseOrder
{
    /// <summary>
    /// Close order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Close order price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Is the close order from liquidation
    /// </summary>
    [JsonProperty("is_liq")]
    public bool IsLiquidated { get; set; }
}