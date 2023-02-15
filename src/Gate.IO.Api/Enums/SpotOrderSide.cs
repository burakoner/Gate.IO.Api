namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Side
/// </summary>
public enum SpotOrderSide
{
    /// <summary>
    /// Enum Buy for value: buy
    /// </summary>
    [EnumMember(Value = "buy")]
    Buy = 1,

    /// <summary>
    /// Enum Sell for value: sell
    /// </summary>
    [EnumMember(Value = "sell")]
    Sell = 2
}