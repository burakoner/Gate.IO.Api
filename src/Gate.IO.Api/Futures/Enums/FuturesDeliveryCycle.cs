namespace Gate.IO.Api.Futures;

public enum FuturesDeliveryCycle
{
    [Map("WEEKLY")]
    Weekly,

    [Map("BI-WEEKLY")]
    BiWeekly,

    [Map("QUARTERLY")]
    Quarterly,

    [Map("BI-QUARTERLY")]
    BiQuarterly,
}