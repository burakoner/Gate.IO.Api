namespace Gate.IO.Api.Enums;

public enum AccountType
{
    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [EnumMember(Value = "spot")]
    Spot,

    /// <summary>
    /// Enum Margin for value: margin
    /// </summary>
    [EnumMember(Value = "margin")]
    Margin,

    /// <summary>
    /// Enum Crossmargin for value: cross_margin
    /// </summary>
    [EnumMember(Value = "cross_margin")]
    CrossMargin,
}

public enum AccountType2
{
    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [EnumMember(Value = "normal")]
    Spot,

    /// <summary>
    /// Enum Margin for value: margin
    /// </summary>
    [EnumMember(Value = "margin")]
    Margin,

    /// <summary>
    /// Enum Crossmargin for value: cross_margin
    /// </summary>
    [EnumMember(Value = "cross_margin")]
    CrossMargin,
}

public enum AccountType3
{
    [EnumMember(Value = "spot")]
    Spot,

    [EnumMember(Value = "futures")]
    Futures,

    [EnumMember(Value = "cross_margin")]
    CrossMargin,
}

public enum WalletAccount
{
    [EnumMember(Value = "spot")]
    Spot,

    [EnumMember(Value = "margin")]
    Margin,

    [EnumMember(Value = "cross_margin")]
    CrossMargin,

    [EnumMember(Value = "futures")]
    Futures,

    [EnumMember(Value = "delivery")]
    Delivery,

    [EnumMember(Value = "options")]
    Options,
}
