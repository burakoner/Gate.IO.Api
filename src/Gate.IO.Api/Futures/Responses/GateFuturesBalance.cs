namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate Futures Account
/// </summary>
public class GateFuturesBalance
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
    /// History
    /// </summary>
    [JsonProperty("history")]
    public GateFuturesBalanceHistory History { get; set; }
}
