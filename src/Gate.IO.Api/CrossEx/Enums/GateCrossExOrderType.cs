namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order type
/// </summary>
public enum GateCrossExOrderType
{
    /// <summary>
    /// Represents the Limit value.
    /// </summary>
    [Map("LIMIT")]
    Limit = 1,

    /// <summary>
    /// Represents the Market value.
    /// </summary>
    [Map("MARKET")]
    Market = 2,
}
