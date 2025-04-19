namespace Gate.IO.Api.Futures;

/// <summary>
/// FuturesBalanceType
/// </summary>
public enum GateFuturesBalanceChangeType : byte
{
    /// <summary>
    /// Deposit &amp; Withdraw
    /// </summary>
    [Map("dnw")]
    DNW = 1,

    /// <summary>
    /// Profit &amp; Loss by reducing position
    /// </summary>
    [Map("pnl")]
    PNL = 2,

    /// <summary>
    /// Trading fee
    /// </summary>
    [Map("fee")]
    Fee = 3,

    /// <summary>
    /// Referrer rebate
    /// </summary>
    [Map("refr")]
    Rebate = 4,

    /// <summary>
    /// Funding
    /// </summary>
    [Map("fund")]
    Funding = 5,

    /// <summary>
    /// POINT Deposit & Withdraw
    /// </summary>
    [Map("point_dnw")]
    PointDNW = 6,

    /// <summary>
    /// POINT Trading fee
    /// </summary>
    [Map("point_fee")]
    PointFee = 7,

    /// <summary>
    /// POINT Referrer rebate
    /// </summary>
    [Map("point_refr")]
    PointRebate = 8,

    /// <summary>
    /// bouns deduction
    /// </summary>
    [Map("bonus_offset")]
    BonusOffset = 9,
}
