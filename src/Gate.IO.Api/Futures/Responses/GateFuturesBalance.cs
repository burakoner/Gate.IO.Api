namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate Futures Account
/// </summary>
public record GateFuturesBalance
{
    /// <summary>
    /// total is the balance after the user&#39;s accumulated deposit, withdraw, profit and loss (including realized profit and loss, fund, fee and referral rebate), excluding unrealized profit and loss.  total &#x3D; SUM(history_dnw, history_pnl, history_fee, history_refr, history_fund)
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }
    
    /// <summary>
    /// Unrealized PNL
    /// </summary>
    [JsonProperty("unrealised_pnl")]
    public decimal UnrealisedPnl { get; set; }
    
    /// <summary>
    /// Position margin
    /// </summary>
    [JsonProperty("position_margin")]
    public decimal PositionMargin { get; set; }
    
    /// <summary>
    /// Order margin of unfinished orders
    /// </summary>
    [JsonProperty("order_margin")]
    public decimal OrderMargin { get; set; }
    
    /// <summary>
    /// The available balance for transferring or trading
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
    /// Whether dual mode is enabled
    /// </summary>
    [JsonProperty("in_dual_mode")]
    public bool InDualMode { get; set; }
    
    /// <summary>
    /// Whether portfolio margin account mode is enabled
    /// </summary>
    [JsonProperty("enable_credit")]
    public bool EnableCredit { get; set; }
    
    /// <summary>
    /// Initial margin position, applicable to the portfolio margin account model
    /// </summary>
    [JsonProperty("position_initial_margin")]
    public decimal PositionInitialMargin { get; set; }
    
    /// <summary>
    /// Maintenance margin position, applicable to the portfolio margin account model
    /// </summary>
    [JsonProperty("maintenance_margin")]
    public decimal MaintenanceMargin { get; set; }
    
    /// <summary>
    /// Perpetual Contract Bonus
    /// </summary>
    [JsonProperty("bonus")]
    public decimal Bonus { get; set; }

    /// <summary>
    /// Classic account margin mode, true - enable new mode, false - revert to old mode.
    /// </summary>
    [JsonProperty("enable_evolved_classic")]
    public bool EnableEvolvedClassic { get; set; }

    /// <summary>
    /// Full -warehouse hanging order deposit, suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_order_margin")]
    public decimal CrossOrderMargin { get; set; }

    /// <summary>
    /// The initial security deposit of the full warehouse is suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_initial_margin")]
    public decimal CrossInitialMargin { get; set; }

    /// <summary>
    /// Maintain deposit in full warehouse, suitable for new classic account margin models
    /// </summary>
    [JsonProperty("cross_maintenance_margin")]
    public decimal CrossMaintenanceMargin { get; set; }

    /// <summary>
    /// The full warehouse does not achieve profit and loss, suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_unrealised_pnl")]
    public decimal CrossUnrealisedPnl { get; set; }

    /// <summary>
    /// Full warehouse available amount, suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_available")]
    public decimal CrossAvailable { get; set; }

    /// <summary>
    /// Full margin balance, suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_margin_balance")]
    public decimal CrossMarginBalance { get; set; }

    /// <summary>
    /// Maintain margin ratio for the full position, suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_mmr")]
    public decimal CrossMmr { get; set; }

    /// <summary>
    /// The initial margin rate of the full position is suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("cross_imr")]
    public decimal CrossImr { get; set; }

    /// <summary>
    /// Ware -position margin, suitable for the new classic account margin model
    /// </summary>
    [JsonProperty("isolated_position_margin")]
    public decimal IsolatedPositionMargin { get; set; }

    /// <summary>
    /// Whether to open a new two-way position mode
    /// </summary>
    [JsonProperty("enable_new_dual_mode")]
    public bool EnableNewDualMode { get; set; }

    /// <summary>
    /// History
    /// </summary>
    [JsonProperty("history")]
    public GateFuturesBalanceHistory History { get; set; }
}
