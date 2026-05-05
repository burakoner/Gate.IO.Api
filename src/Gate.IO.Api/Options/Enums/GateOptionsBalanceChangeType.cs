namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsBalanceType
/// </summary>
public enum GateOptionsBalanceChangeType : byte
{
    /// <summary>
    /// Deposit &amp; Withdraw
    /// </summary>
    [Map("dnw")]
    DNW = 1,

    /// <summary>
    /// Trading premium
    /// </summary>
    [Map("prem")]
    Premium = 2,

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
    /// Settlement PNL
    /// </summary>
    [Map("set")]
    SettlementPNL = 5,

    /// <summary>
    /// POINT Deposit &amp; Withdraw
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
}
