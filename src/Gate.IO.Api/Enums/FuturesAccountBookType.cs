namespace Gate.IO.Api.Enums;

public enum FuturesAccountBookType
{
    /// <summary>
    /// Deposit & Withdraw
    /// </summary>
    [EnumMember(Value = "dnw")]
    DNW,

    /// <summary>
    /// Profit & Loss by reducing position
    /// </summary>
    [EnumMember(Value = "pnl")]
    PNL,

    /// <summary>
    /// Trading fee
    /// </summary>
    [EnumMember(Value = "fee")]
    Fee,

    /// <summary>
    /// Referrer rebate
    /// </summary>
    [EnumMember(Value = "refr")]
    Rebate,

    /// <summary>
    /// Funding
    /// </summary>
    [EnumMember(Value = "fund")]
    Funding,

    /// <summary>
    /// POINT Deposit & Withdraw
    /// </summary>
    [EnumMember(Value = "point_dnw")]
    PointDNW,

    /// <summary>
    /// POINT Trading fee
    /// </summary>
    [EnumMember(Value = "point_fee")]
    PointFee,

    /// <summary>
    /// POINT Referrer rebate
    /// </summary>
    [EnumMember(Value = "point_refr")]
    PointRebate,
}
