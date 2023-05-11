namespace Gate.IO.Api.Enums;

public enum FuturesAccountBookType
{
    /// <summary>
    /// Deposit & Withdraw
    /// </summary>
    [Label("dnw")]
    DNW,

    /// <summary>
    /// Profit & Loss by reducing position
    /// </summary>
    [Label("pnl")]
    PNL,

    /// <summary>
    /// Trading fee
    /// </summary>
    [Label("fee")]
    Fee,

    /// <summary>
    /// Referrer rebate
    /// </summary>
    [Label("refr")]
    Rebate,

    /// <summary>
    /// Funding
    /// </summary>
    [Label("fund")]
    Funding,

    /// <summary>
    /// POINT Deposit & Withdraw
    /// </summary>
    [Label("point_dnw")]
    PointDNW,

    /// <summary>
    /// POINT Trading fee
    /// </summary>
    [Label("point_fee")]
    PointFee,

    /// <summary>
    /// POINT Referrer rebate
    /// </summary>
    [Label("point_refr")]
    PointRebate,
}
