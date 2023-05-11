namespace Gate.IO.Api.Enums;

public enum DeliveryCycle
{
    [Label("WEEKLY")]
    Weekly,

    [Label("BI-WEEKLY")]
    BiWeekly,

    [Label("QUARTERLY")]
    Quarterly,

    [Label("BI-QUARTERLY")]
    BiQuarterly,
}