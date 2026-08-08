namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock trading status
/// </summary>
public enum GateStockTradingStatus
{
    /// <summary>Pre-market session</summary>
    [Map("pre_market")]
    PreMarket,
    /// <summary>Regular session</summary>
    [Map("open")]
    Open,
    /// <summary>Post-market session</summary>
    [Map("post_market")]
    PostMarket,
    /// <summary>Market closed</summary>
    [Map("closed")]
    Closed,
    /// <summary>Gate liquidity-provider session</summary>
    [Map("gt_lp")]
    GateLiquidityProvider,
}
