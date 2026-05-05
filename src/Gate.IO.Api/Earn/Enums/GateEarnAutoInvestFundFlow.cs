namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest fund flow direction
/// </summary>
public enum GateEarnAutoInvestFundFlow : byte
{
    /// <summary>
    /// Auto invest
    /// </summary>
    [Map("auto_invest")]
    AutoInvest = 1,

    /// <summary>
    /// Flexible savings
    /// </summary>
    [Map("earn")]
    Earn = 2,
}
