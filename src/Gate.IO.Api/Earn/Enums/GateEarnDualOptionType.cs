namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment option type
/// </summary>
public enum GateEarnDualOptionType : byte
{
    /// <summary>
    /// Buy low
    /// </summary>
    [Map("put")]
    Put = 1,

    /// <summary>
    /// Sell high
    /// </summary>
    [Map("call")]
    Call = 2,
}
