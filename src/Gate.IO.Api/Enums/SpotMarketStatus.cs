namespace Gate.IO.Api.Enums;

/// <summary>
/// How currency pair can be traded  - untradable: cannot be bought or sold - buyable: can be bought - sellable: can be sold - tradable: can be bought or sold
/// </summary>
public enum SpotMarketStatus
{
    /// <summary>
    /// Enum Untradable for value: untradable
    /// </summary>
    [EnumMember(Value = "untradable")]
    Untradable = 1,

    /// <summary>
    /// Enum Buyable for value: buyable
    /// </summary>
    [EnumMember(Value = "buyable")]
    Buyable = 2,

    /// <summary>
    /// Enum Sellable for value: sellable
    /// </summary>
    [EnumMember(Value = "sellable")]
    Sellable = 3,

    /// <summary>
    /// Enum Tradable for value: tradable
    /// </summary>
    [EnumMember(Value = "tradable")]
    Tradable = 4,
}
