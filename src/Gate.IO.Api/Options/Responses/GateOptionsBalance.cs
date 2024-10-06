namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsBalance
/// </summary>
public class GateOptionsBalance
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }
    
    /// <summary>
    /// Total account balance
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// 仓位价值，做多仓位价值为正，做空仓位价值为负
    /// </summary>
    [JsonProperty("position_value")]
    public decimal PositionValue { get; set; }

    /// <summary>
    /// 账户权益，账户余额与仓位价值的和
    /// </summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }
    
    /// <summary>
    /// If the account is allowed to short
    /// </summary>
    [JsonProperty("short_enabled")]
    public bool ShortEnabled { get; set; }

    /// <summary>
    /// 是否启用MMP
    /// </summary>
    [JsonProperty("mmp_enabled")]
    public bool MmpEnabled { get; set; }

    /// <summary>
    /// 是否触发仓位强平
    /// </summary>
    [JsonProperty("liq_triggered")]
    public bool Liqtriggered { get; set; }

    /// <summary>
    /// ｜ 保证金模式：
    /// - 0：经典现货保证金模式 - 1：跨币种保证金模式 - 2：组合保证金模式
    /// </summary>
    [JsonProperty("margin_mode")]
    public int MarginMode { get; set; }
    
    /// <summary>
    /// Unrealized PNL
    /// </summary>
    [JsonProperty("unrealised_pnl")]
    public decimal UnrealisedPnl { get; set; }
    
    /// <summary>
    /// Initial position margin
    /// </summary>
    [JsonProperty("init_margin")]
    public decimal InitMargin { get; set; }
    
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
    /// 未完成卖单的保证金
    /// </summary>
    [JsonProperty("ask_order_margin")]
    public decimal AskOrderMargin { get; set; }

    /// <summary>
    /// 未完成买单的保证金
    /// </summary>
    [JsonProperty("bid_order_margin")]
    public decimal BidOrderMargin { get; set; }
    
    /// <summary>
    /// Available balance to transfer out or trade
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }
    
    /// <summary>
    /// POINT amount
    /// </summary>
    [JsonProperty("point")]
    public decimal Point { get; set; }

    /// <summary>
    /// Settle currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    /// <summary>
    /// 未完成订单数量上限
    /// </summary>
    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }

    /// <summary>
    /// 名义价值上限，包含仓位以及未完成订单的名义价值
    /// </summary>
    [JsonProperty("position_notional_limit")]
    public long PositionNotionalLimit { get; set; }
}