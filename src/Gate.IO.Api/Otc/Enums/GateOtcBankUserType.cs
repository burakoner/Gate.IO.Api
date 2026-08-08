namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank supplement verification type
/// </summary>
public enum GateOtcBankUserType : byte
{
    /// <summary>
    /// Personal verification
    /// </summary>
    [Map("personal")]
    Personal = 1,

    /// <summary>
    /// Enterprise verification
    /// </summary>
    [Map("enterprise")]
    Enterprise = 2,
}
