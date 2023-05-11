namespace Gate.IO.Api.Enums;

public enum SpotOrderUpdateEvent
{
    [Label("put")]
    Put,

    [Label("update")]
    Update,

    [Label("finish")]
    Finish,
}