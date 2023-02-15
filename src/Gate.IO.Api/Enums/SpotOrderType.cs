namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Type
/// </summary>
public enum SpotOrderType
{
    /// <summary>
    /// Limit Order
    /// </summary>
    [EnumMember(Value = "limit")]
    Limit = 1,

    /// <summary>
    /// Market Order
    /// </summary>
    [EnumMember(Value = "market")]
    Market = 2
}