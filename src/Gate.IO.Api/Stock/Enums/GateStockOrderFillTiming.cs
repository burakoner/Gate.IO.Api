namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order fill timing
/// </summary>
public enum GateStockOrderFillTiming
{
    /// <summary>Fill immediately</summary>
    [Map("1")]
    Immediate = 1,
    /// <summary>Fill after pre-market opens</summary>
    [Map("2")]
    AfterPreMarketOpen = 2,
    /// <summary>Fill after the regular market opens</summary>
    [Map("3")]
    AfterRegularMarketOpen = 3,
}
