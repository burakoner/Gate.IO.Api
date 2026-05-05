namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order activation price type
/// </summary>
public enum GateFuturesTrailPriceType : byte
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Latest price
    /// </summary>
    Latest = 1,

    /// <summary>
    /// Index price
    /// </summary>
    Index = 2,

    /// <summary>
    /// Mark price
    /// </summary>
    Mark = 3,
}
