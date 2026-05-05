namespace Gate.IO.Api.Bot;

/// <summary>
/// Contract martingale creation parameters
/// </summary>
public record GateBotContractMartingaleCreateParameters
{
    public decimal InvestAmount { get; set; }

    public decimal PriceDeviation { get; set; }

    public int MaxOrders { get; set; }

    public decimal TakeProfitRatio { get; set; }

    public GateBotContractMartingaleDirection Direction { get; set; }

    public decimal Leverage { get; set; }

    public decimal? StopLossPrice { get; set; }

    public decimal? ProfitSharingRatio { get; set; }
}
