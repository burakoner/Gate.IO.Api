namespace Gate.IO.Api.Enums;

public enum AccountType
{
    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [Label("spot")]
    Spot,

    /// <summary>
    /// Enum Margin for value: margin
    /// </summary>
    [Label("margin")]
    Margin,

    /// <summary>
    /// Enum Crossmargin for value: cross_margin
    /// </summary>
    [Label("cross_margin")]
    CrossMargin,
}

public enum AccountType2
{
    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [Label("normal")]
    Spot,

    /// <summary>
    /// Enum Margin for value: margin
    /// </summary>
    [Label("margin")]
    Margin,

    /// <summary>
    /// Enum Crossmargin for value: cross_margin
    /// </summary>
    [Label("cross_margin")]
    CrossMargin,
}

public enum AccountType3
{
    [Label("spot")]
    Spot,

    [Label("futures")]
    Futures,

    [Label("cross_margin")]
    CrossMargin,
}

public enum WalletAccount
{
    [Label("spot")]
    Spot,

    [Label("margin")]
    Margin,

    [Label("cross_margin")]
    CrossMargin,

    [Label("futures")]
    Futures,

    [Label("delivery")]
    Delivery,

    [Label("options")]
    Options,
}
