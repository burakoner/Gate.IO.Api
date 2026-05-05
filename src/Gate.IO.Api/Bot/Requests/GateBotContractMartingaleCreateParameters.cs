namespace Gate.IO.Api.Bot;

/// <summary>
/// Contract martingale creation parameters
/// </summary>
public record GateBotContractMartingaleCreateParameters
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
    /// Gets or sets the Direction.
    /// </summary>
    public GateBotContractMartingaleDirection Direction { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    public decimal Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Stop Loss Price.
    /// </summary>
    public decimal? StopLossPrice { get; set; }

    /// <summary>
    /// Gets or sets the Profit Sharing Ratio.
    /// </summary>
    public decimal? ProfitSharingRatio { get; set; }
}
