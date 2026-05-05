namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni interest status
/// </summary>
public enum GateEarnUniInterestStatus : byte
{
    /// <summary>
    /// Normal dividend
    /// </summary>
    [Map("interest_dividend")]
    Dividend = 1,

    /// <summary>
    /// Interest reinvestment
    /// </summary>
    [Map("interest_reinvest")]
    Reinvest = 2,
}
