namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot open orders query request
/// </summary>
public record GateSpotOpenOrdersRequest
{
    /// <summary>
    /// Account type
    /// </summary>
    public GateSpotAccountType? Account { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }
}
