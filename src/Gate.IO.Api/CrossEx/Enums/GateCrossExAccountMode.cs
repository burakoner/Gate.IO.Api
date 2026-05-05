namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account mode
/// </summary>
public enum GateCrossExAccountMode
{
    /// <summary>
    /// Represents the Cross Exchange value.
    /// </summary>
    [Map("CROSS_EXCHANGE")]
    CrossExchange = 1,

    /// <summary>
    /// Represents the Isolated Exchange value.
    /// </summary>
    [Map("ISOLATED_EXCHANGE")]
    IsolatedExchange = 2,
}
