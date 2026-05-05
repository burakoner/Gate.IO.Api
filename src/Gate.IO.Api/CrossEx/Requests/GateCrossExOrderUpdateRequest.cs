namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order update request
/// </summary>
public record GateCrossExOrderUpdateRequest
{
    /// <summary>
    /// Modified quantity
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Modified price
    /// </summary>
    public decimal? Price { get; set; }
}
