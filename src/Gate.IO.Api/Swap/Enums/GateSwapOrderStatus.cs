namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate Swap Order Status
/// </summary>
public enum GateSwapOrderStatus : byte
{
    /// <summary>
    /// Success
    /// </summary>
    [Map("1")]
    Success = 1,

    /// <summary>
    /// Failure
    /// </summary>
    [Map("2")]
    Failure = 2,
}