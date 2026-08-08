namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order sort field
/// </summary>
public enum GateFuturesChaseOrderSort : byte
{
    /// <summary>
    /// Sort by creation time
    /// </summary>
    CreatedAt = 1,

    /// <summary>
    /// Sort by finish time
    /// </summary>
    FinishedAt = 2,
}
