namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment product sort
/// </summary>
public enum GateEarnDualPlanSort : byte
{
    /// <summary>
    /// Highest APY first
    /// </summary>
    [Map("apy")]
    Apy = 1,

    /// <summary>
    /// Shortest tenor first
    /// </summary>
    [Map("short-period")]
    ShortPeriod = 2,

    /// <summary>
    /// Highest premium first
    /// </summary>
    [Map("multiple")]
    Multiple = 3,
}
