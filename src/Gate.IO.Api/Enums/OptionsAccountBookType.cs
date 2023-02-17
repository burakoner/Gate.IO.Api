namespace Gate.IO.Api.Enums;

public enum OptionsAccountBookType
{
    /// <summary>
    /// Deposit & Withdraw
    /// </summary>
    [EnumMember(Value = "dnw")]
    DNW,

    /// <summary>
    /// Trading premium
    /// </summary>
    [EnumMember(Value = "prem")]
    Premium,

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
    /// POINT Referrer rebate
    /// </summary>
    [EnumMember(Value = "set")]
    SettlementPNL,
}
