namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate Swap Order Status
/// </summary>
public enum GateSwapOrderStatus
{
    /// <summary>
    /// Success
    /// </summary>
    [Map("1")]
    Success,

    /// <summary>
    /// Failure
    /// </summary>
    [Map("2")]
    Failure,
}