namespace Gate.IO.Api.Enums;

public enum OptionsAccountBookType
{
    /// <summary>
    /// Deposit & Withdraw
    /// </summary>
    [Label("dnw")]
    DNW,

    /// <summary>
    /// Trading premium
    /// </summary>
    [Label("prem")]
    Premium,

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
    /// POINT Referrer rebate
    /// </summary>
    [Label("set")]
    SettlementPNL,
}
