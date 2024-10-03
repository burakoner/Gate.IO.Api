namespace Gate.IO.Api.Futures;

public enum FuturesTriggerOrderStrategyType
{
    [Map("0")]
    ByPrice=1,

    [Map("1")]
    ByPriceGap = 1,
}