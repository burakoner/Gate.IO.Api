namespace Gate.IO.Api.Futures;

/// <summary>
/// Mark price type, internal - based on internal trading, index - based on external index price
/// </summary>
public enum FuturesMarkType
{
    /// <summary>
    /// Enum Internal for value: internal
    /// </summary>
    [Map("internal")]
    Internal,

    /// <summary>
    /// Enum Index for value: index
    /// </summary>
    [Map("index")]
    Index
}