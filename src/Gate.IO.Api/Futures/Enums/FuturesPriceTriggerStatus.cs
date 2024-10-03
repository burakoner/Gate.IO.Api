namespace Gate.IO.Api.Futures;

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
