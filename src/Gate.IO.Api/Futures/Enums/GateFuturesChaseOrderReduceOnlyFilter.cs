namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order reduce-only filter
/// </summary>
public enum GateFuturesChaseOrderReduceOnlyFilter : byte
{
    /// <summary>
    /// Return reduce-only orders
    /// </summary>
    ReduceOnly = 1,

    /// <summary>
    /// Return orders that are not reduce-only
    /// </summary>
    NotReduceOnly = 2,
}
