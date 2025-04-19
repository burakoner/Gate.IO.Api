namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesTriggerStrategy
/// </summary>
public enum GateFuturesTriggerStrategy : byte
{
    /// <summary>
    /// ByPrice
    /// </summary>
    [Map("0")]
    ByPrice = 0,

    /// <summary>
    /// ByPriceGap
    /// </summary>
    [Map("1")]
    ByPriceGap = 1,
}