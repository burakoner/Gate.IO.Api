namespace Gate.IO.Api.Enums;

public enum FuturesTriggerOrderPriceType
{
    /// <summary>
    /// Latest Deal Price
    /// </summary>
    [Label("0")]
    DealPrice=0,

    /// <summary>
    /// Mark Price
    /// </summary>
    [Label("1")]
    MarkPrice=1,

    /// <summary>
    /// Index Price
    /// </summary>
    [Label("2")]
    IndexPrice=2,
}