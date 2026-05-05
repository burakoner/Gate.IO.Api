namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot grid creation parameters
/// </summary>
public record GateBotSpotGridCreateParameters
{
    /// <summary>
    /// Gets or sets the Money.
    /// </summary>
    public decimal Money { get; set; }

    /// <summary>
    /// Gets or sets the Low Price.
    /// </summary>
    public decimal LowPrice { get; set; }

    /// <summary>
    /// Gets or sets the High Price.
    /// </summary>
    public decimal HighPrice { get; set; }

    /// <summary>
    /// Gets or sets the Grid Number.
    /// </summary>
    public int GridNumber { get; set; }

    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    public GateBotGridPriceType PriceType { get; set; }

    /// <summary>
    /// Gets or sets the Trigger Price.
    /// </summary>
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Gets or sets the Stop Profit.
    /// </summary>
    public decimal? StopProfit { get; set; }

    /// <summary>
    /// Gets or sets the Stop Loss.
    /// </summary>
    public decimal? StopLoss { get; set; }

    /// <summary>
    /// Gets or sets the Profit Sharing Ratio.
    /// </summary>
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Gets or sets the Is Use Base.
    /// </summary>
    public bool? IsUseBase { get; set; }
}
