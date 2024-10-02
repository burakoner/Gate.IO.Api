namespace Gate.IO.Api.Enums;

public enum DeliveryCycle
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