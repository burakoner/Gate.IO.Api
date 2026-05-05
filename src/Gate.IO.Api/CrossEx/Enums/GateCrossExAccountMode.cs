namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account mode
/// </summary>
public enum GateCrossExAccountMode
{
    [Map("CROSS_EXCHANGE")]
    CrossExchange = 1,

    [Map("ISOLATED_EXCHANGE")]
    IsolatedExchange = 2,
}
