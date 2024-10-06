namespace Gate.IO.Api.Futures;

/// <summary>
/// FuturesBalanceType
/// </summary>
public enum GateFuturesBalanceChangeType
{
    /// <summary>
    /// Deposit & Withdraw
    /// </summary>
    [Map("dnw")]
    DNW,

    /// <summary>
    /// Profit & Loss by reducing position
    /// </summary>
    [Map("pnl")]
    PNL,

    /// <summary>
    /// Trading fee
    /// </summary>
    [Map("fee")]
    Fee,

    /// <summary>
    /// Referrer rebate
    /// </summary>
    [Map("refr")]
    Rebate,

    /// <summary>
    /// Funding
    /// </summary>
    [Map("fund")]
    Funding,

    /// <summary>
    /// POINT Deposit & Withdraw
    /// </summary>
    [Map("point_dnw")]
    PointDNW,

    /// <summary>
    /// POINT Trading fee
    /// </summary>
    [Map("point_fee")]
    PointFee,

    /// <summary>
    /// POINT Referrer rebate
    /// </summary>
    [Map("point_refr")]
    PointRebate,

    /// <summary>
    /// bouns deduction
    /// </summary>
    [Map("bonus_offset")]
    BonusOffset,
}
