namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx business type
/// </summary>
public enum GateCrossExBusinessType
{
    [Map("SPOT")]
    Spot = 1,

    [Map("FUTURE")]
    Future = 2,

    [Map("MARGIN")]
    Margin = 3,
}
