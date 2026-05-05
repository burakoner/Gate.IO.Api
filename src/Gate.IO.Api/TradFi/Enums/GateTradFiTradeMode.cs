namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading mode
/// </summary>
public enum GateTradFiTradeMode
{
    /// <summary>
    /// Disabled
    /// </summary>
    [Map("0")]
    Disabled = 0,

    /// <summary>
    /// Long only
    /// </summary>
    [Map("1")]
    LongOnly = 1,

    /// <summary>
    /// Short only
    /// </summary>
    [Map("2")]
    ShortOnly = 2,

    /// <summary>
    /// Close only
    /// </summary>
    [Map("3")]
    CloseOnly = 3,

    /// <summary>
    /// Full trading access
    /// </summary>
    [Map("4")]
    Full = 4,
}
