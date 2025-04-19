namespace Gate.IO.Api.Spot;

/// <summary>
/// How currency pair can be traded  - untradable: cannot be bought or sold - buyable: can be bought - sellable: can be sold - tradable: can be bought or sold
/// </summary>
public enum GateSpotMarketStatus : byte
{
    /// <summary>
    /// Enum Untradable for value: untradable
    /// </summary>
    [Map("untradable")]
    Untradable = 1,

    /// <summary>
    /// Enum Buyable for value: buyable
    /// </summary>
    [Map("buyable")]
    Buyable = 2,

    /// <summary>
    /// Enum Sellable for value: sellable
    /// </summary>
    [Map("sellable")]
    Sellable = 3,

    /// <summary>
    /// Enum Tradable for value: tradable
    /// </summary>
    [Map("tradable")]
    Tradable = 4,
}
