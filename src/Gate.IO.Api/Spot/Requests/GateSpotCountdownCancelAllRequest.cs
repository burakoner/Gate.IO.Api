namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot countdown cancel all request
/// </summary>
public record GateSpotCountdownCancelAllRequest
{
    /// <summary>
    /// Countdown time in seconds
    /// </summary>
    public int Timeout { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }
}
