namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest fund source
/// </summary>
public enum GateEarnAutoInvestFundSource : byte
{
    /// <summary>
    /// Spot account
    /// </summary>
    [Map("spot")]
    Spot = 1,

    /// <summary>
    /// Flexible savings
    /// </summary>
    [Map("earn")]
    Earn = 2,
}
