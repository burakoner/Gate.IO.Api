namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx full close position request
/// </summary>
public record GateCrossExClosePositionRequest
{
    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    public GateCrossExPositionSide? PositionSide { get; set; }
}
