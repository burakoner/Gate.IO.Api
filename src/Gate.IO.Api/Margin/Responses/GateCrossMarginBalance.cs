namespace Gate.IO.Api.Margin;

/// <summary>
/// GateCrossMarginBalance
/// </summary>
public class GateCrossMarginBalance
{
    /// <summary>
    /// Available amount
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount
    /// </summary>
    [JsonProperty("freeze")]
    public decimal Frozen { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Unpaid interests
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
    
    /// <summary>
    /// Negative Liabilities. Formula:Min[available+total+unrealized_pnl,0]
    /// </summary>
    [JsonProperty("negative_liab")]
    public decimal NegativeLiabilities { get; set; }
    
    /// <summary>
    /// Borrowing to Open Positions in Futures
    /// </summary>
    [JsonProperty("futures_pos_liab")]
    public decimal FuturesOpenPositionsLiabilities { get; set; }
    
    /// <summary>
    /// Equity. Formula: available + freeze - borrowed + futures account's total + unrealized_pnl
    /// </summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }
    
    /// <summary>
    /// Total freeze. Formula: freeze + position_initial_margin + order_margin
    /// </summary>
    [JsonProperty("total_freeze")]
    public decimal TotalFreeze { get; set; }
    
    /// <summary>
    /// Total liabilities. Formula: Max[Abs[Min[quity - total_freeze,0], borrowed]] - futures_pos_liab
    /// </summary>
    [JsonProperty("total_liab")]
    public decimal TotalLiabilities { get; set; }
}
