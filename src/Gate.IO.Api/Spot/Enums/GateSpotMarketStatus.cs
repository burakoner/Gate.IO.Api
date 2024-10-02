namespace Gate.IO.Api.Spot;

/// <summary>
/// How currency pair can be traded  - untradable: cannot be bought or sold - buyable: can be bought - sellable: can be sold - tradable: can be bought or sold
/// </summary>
public enum GateSpotMarketStatus
{
    /// <summary>
    /// Enum Untradable for value: untradable
    /// </summary>
    [Map("untradable")]
    Untradable,

    /// <summary>
    /// Enum Buyable for value: buyable
    /// </summary>
    [Map("buyable")]
    Buyable,

    /// <summary>
    /// Enum Sellable for value: sellable
    /// </summary>
    [Map("sellable")]
    Sellable,

    /// <summary>
    /// Enum Tradable for value: tradable
    /// </summary>
    [Map("tradable")]
    Tradable,
}
