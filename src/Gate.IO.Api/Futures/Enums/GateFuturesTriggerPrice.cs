namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesTriggerPrice
/// </summary>
public enum GateFuturesTriggerPrice
{
    /// <summary>
    /// Latest Deal Price
    /// </summary>
    [Map("0")]
    DealPrice = 0,

    /// <summary>
    /// Mark Price
    /// </summary>
    [Map("1")]
    MarkPrice = 1,

    /// <summary>
    /// Index Price
    /// </summary>
    [Map("2")]
    IndexPrice = 2,
}