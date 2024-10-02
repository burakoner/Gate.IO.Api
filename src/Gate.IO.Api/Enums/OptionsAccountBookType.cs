namespace Gate.IO.Api.Enums;

public enum OptionsAccountBookType
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
