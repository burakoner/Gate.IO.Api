namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot martingale creation parameters
/// </summary>
public record GateBotSpotMartingaleCreateParameters
{
    public decimal InvestAmount { get; set; }

    public decimal PriceDeviation { get; set; }

    public int MaxOrders { get; set; }

    public decimal TakeProfitRatio { get; set; }

    public decimal? StopLossPerCycle { get; set; }

    public decimal? TriggerPrice { get; set; }

    public decimal? ProfitSharingRatio { get; set; }
}
