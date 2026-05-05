namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order type
/// </summary>
public enum GateCrossExOrderType
{
    [Map("LIMIT")]
    Limit = 1,

    [Map("MARKET")]
    Market = 2,
}
