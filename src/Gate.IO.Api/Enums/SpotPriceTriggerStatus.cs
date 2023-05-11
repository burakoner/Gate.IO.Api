namespace Gate.IO.Api.Enums;

public enum SpotPriceTriggerStatus
{
    [Label("open")]
    Open,

    [Label("cancelled")]
    Cancelled,

    [Label("finish")]
    Finished,

    [Label("failed")]
    Failed,

    [Label("expired")]
    Expired,
}
