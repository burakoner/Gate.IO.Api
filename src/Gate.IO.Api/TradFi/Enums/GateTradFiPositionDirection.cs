namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi position direction
/// </summary>
public enum GateTradFiPositionDirection
{
    /// <summary>
    /// Represents the Long value.
    /// </summary>
    [Map("Long")]
    Long = 1,

    /// <summary>
    /// Represents the Short value.
    /// </summary>
    [Map("Short")]
    Short = 2,
}
