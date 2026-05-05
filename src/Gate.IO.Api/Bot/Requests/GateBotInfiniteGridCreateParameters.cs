namespace Gate.IO.Api.Bot;

/// <summary>
/// Infinite grid creation parameters
/// </summary>
public record GateBotInfiniteGridCreateParameters
{
    public decimal Money { get; set; }

    public decimal PriceFloor { get; set; }

    public decimal ProfitPerGrid { get; set; }

    public int? GridNumber { get; set; }

    public GateBotGridPriceType? PriceType { get; set; }

    public decimal? TriggerPrice { get; set; }

    public decimal? StopProfit { get; set; }

    public decimal? StopLoss { get; set; }

    public decimal? ProfitSharingRatio { get; set; }

    public bool? IsUseBase { get; set; }
}
