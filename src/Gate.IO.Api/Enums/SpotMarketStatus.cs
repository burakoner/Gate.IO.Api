namespace Gate.IO.Api.Enums;

/// <summary>
/// How currency pair can be traded  - untradable: cannot be bought or sold - buyable: can be bought - sellable: can be sold - tradable: can be bought or sold
/// </summary>
public enum SpotMarketStatus
{
    /// <summary>
    /// Enum Untradable for value: untradable
    /// </summary>
    [Label("untradable")]
    Untradable,

    /// <summary>
    /// Enum Buyable for value: buyable
    /// </summary>
    [Label("buyable")]
    Buyable,

    /// <summary>
    /// Enum Sellable for value: sellable
    /// </summary>
    [Label("sellable")]
    Sellable,

    /// <summary>
    /// Enum Tradable for value: tradable
    /// </summary>
    [Label("tradable")]
    Tradable,
}
