namespace Gate.IO.Api.Futures;

/// <summary>
/// Mark price type, internal - based on internal trading, index - based on external index price
/// </summary>
public enum GateFuturesMarkType
{
    /// <summary>
    /// Enum Internal for value: internal
    /// </summary>
    [Map("internal")]
    Internal = 1,

    /// <summary>
    /// Enum Index for value: index
    /// </summary>
    [Map("index")]
    Index = 2
}