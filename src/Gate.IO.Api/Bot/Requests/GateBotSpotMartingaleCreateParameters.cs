namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot martingale creation parameters
/// </summary>
public record GateBotSpotMartingaleCreateParameters
{
    /// <summary>
    /// Gets or sets the Invest Amount.
    /// </summary>
    public decimal InvestAmount { get; set; }

    /// <summary>
    /// Gets or sets the Price Deviation.
    /// </summary>
    public decimal PriceDeviation { get; set; }

    /// <summary>
    /// Gets or sets the Max Orders.
    /// </summary>
    public int MaxOrders { get; set; }

    /// <summary>
    /// Gets or sets the Take Profit Ratio.
    /// </summary>
    public decimal TakeProfitRatio { get; set; }

    /// <summary>
    /// Gets or sets the Stop Loss Per Cycle.
    /// </summary>
    public decimal? StopLossPerCycle { get; set; }

    /// <summary>
    /// Gets or sets the Trigger Price.
    /// </summary>
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Gets or sets the Profit Sharing Ratio.
    /// </summary>
    public decimal? ProfitSharingRatio { get; set; }
}
