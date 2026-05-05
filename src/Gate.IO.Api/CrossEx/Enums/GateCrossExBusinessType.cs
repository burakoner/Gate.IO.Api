namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx business type
/// </summary>
public enum GateCrossExBusinessType
{
    /// <summary>
    /// Represents the Spot value.
    /// </summary>
    [Map("SPOT")]
    Spot = 1,

    /// <summary>
    /// Represents the Future value.
    /// </summary>
    [Map("FUTURE")]
    Future = 2,

    /// <summary>
    /// Represents the Margin value.
    /// </summary>
    [Map("MARGIN")]
    Margin = 3,
}
