namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy type
/// </summary>
public enum GateBotStrategyType : byte
{
    [Map("spot_grid")]
    SpotGrid = 1,

    [Map("margin_grid")]
    MarginGrid = 2,

    [Map("infinite_grid")]
    InfiniteGrid = 3,

    [Map("futures_grid")]
    FuturesGrid = 4,

    [Map("spot_martingale")]
    SpotMartingale = 5,

    [Map("contract_martingale")]
    ContractMartingale = 6,
}
