namespace Gate.IO.Api.Spot;

/// <summary>
/// Target participation rate for a Spot POV order
/// </summary>
public enum GateSpotPovParticipationRate
{
    /// <summary>
    /// Five percent
    /// </summary>
    FivePercent = 5,

    /// <summary>
    /// Ten percent
    /// </summary>
    TenPercent = 10,

    /// <summary>
    /// Twenty percent
    /// </summary>
    TwentyPercent = 20,

    /// <summary>
    /// Forty percent
    /// </summary>
    FortyPercent = 40,
}
