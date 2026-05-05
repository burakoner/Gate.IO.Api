namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy type
/// </summary>
public enum GateBotStrategyType : byte
{
    /// <summary>
    /// Represents the Spot Grid value.
    /// </summary>
    [Map("spot_grid")]
    SpotGrid = 1,

    /// <summary>
    /// Represents the Margin Grid value.
    /// </summary>
    [Map("margin_grid")]
    MarginGrid = 2,

    /// <summary>
    /// Represents the Infinite Grid value.
    /// </summary>
    [Map("infinite_grid")]
    InfiniteGrid = 3,

    /// <summary>
    /// Represents the Futures Grid value.
    /// </summary>
    [Map("futures_grid")]
    FuturesGrid = 4,

    /// <summary>
    /// Represents the Spot Martingale value.
    /// </summary>
    [Map("spot_martingale")]
    SpotMartingale = 5,

    /// <summary>
    /// Represents the Contract Martingale value.
    /// </summary>
    [Map("contract_martingale")]
    ContractMartingale = 6,
}
