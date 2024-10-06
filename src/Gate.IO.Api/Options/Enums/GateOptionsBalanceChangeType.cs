namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsBalanceType
/// </summary>
public enum GateOptionsBalanceChangeType
{
    /// <summary>
    /// Deposit & Withdraw
    /// </summary>
    [Map("dnw")]
    DNW,

    /// <summary>
    /// Trading premium
    /// </summary>
    [Map("prem")]
    Premium,

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
    /// POINT Referrer rebate
    /// </summary>
    [Map("set")]
    SettlementPNL,
}
