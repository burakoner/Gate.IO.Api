namespace Gate.IO.Api.Enums;

public enum AccountType2
{
    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [Map("normal")]
    Spot,

    /// <summary>
    /// Enum Margin for value: margin
    /// </summary>
    [Map("margin")]
    Margin,

    /// <summary>
    /// Enum Crossmargin for value: cross_margin
    /// </summary>
    [Map("cross_margin")]
    CrossMargin,
}

public enum AccountType3
{
    [Map("spot")]
    Spot,

    [Map("futures")]
    Futures,

    [Map("cross_margin")]
    CrossMargin,
}
