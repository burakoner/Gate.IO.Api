namespace Gate.IO.Api.Bot;

/// <summary>
/// Margin grid creation parameters
/// </summary>
public record GateBotMarginGridCreateParameters
{
    public decimal Money { get; set; }

    public decimal LowPrice { get; set; }

    public decimal HighPrice { get; set; }

    public int GridNumber { get; set; }

    public GateBotGridPriceType PriceType { get; set; }

    public decimal Leverage { get; set; }

    public GateBotFuturesDirection? Direction { get; set; }

    public decimal? TriggerPrice { get; set; }

    public decimal? StopProfit { get; set; }

    public decimal? StopLoss { get; set; }

    public decimal? ProfitSharingRatio { get; set; }

    public bool? IsUseBase { get; set; }
}
