namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi position direction
/// </summary>
public enum GateTradFiPositionDirection
{
    [Map("Long")]
    Long = 1,

    [Map("Short")]
    Short = 2,
}
