namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot POV order list query
/// </summary>
public record GateSpotPovOrderQueryRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Active or finished order filter. Defaults to active orders.
    /// </summary>
    public GateSpotOrderQueryStatus Status { get; set; } = GateSpotOrderQueryStatus.Open;

    /// <summary>
    /// Buy or sell side. Both are returned when omitted.
    /// </summary>
    public GateSpotOrderSide? Side { get; set; }

    /// <summary>
    /// Page number, from 1 through 100
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list. Defaults to 100 when omitted; maximum 1000.
    /// </summary>
    public int? Limit { get; set; }
}
