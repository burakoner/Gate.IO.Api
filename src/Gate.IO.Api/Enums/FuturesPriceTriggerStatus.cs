namespace Gate.IO.Api.Enums;

public enum FuturesPriceTriggerStatus
{
    [Map("open")]
    Open,

    [Map("finished")]
    Finished,

    [Map("inactive")]
    Inactive,

    [Map("invalid")]
    Invalid,
}
