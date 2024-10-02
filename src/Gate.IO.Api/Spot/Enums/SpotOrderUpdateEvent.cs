namespace Gate.IO.Api.Spot;

public enum SpotOrderUpdateEvent
{
    [Map("put")]
    Put,

    [Map("update")]
    Update,

    [Map("finish")]
    Finish,
}