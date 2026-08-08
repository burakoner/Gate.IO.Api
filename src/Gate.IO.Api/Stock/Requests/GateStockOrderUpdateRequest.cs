namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order update request
/// </summary>
public record GateStockOrderUpdateRequest
{
    /// <summary>Gets or sets the new volume.</summary>
    public decimal Volume { get; set; }
    /// <summary>Gets or sets the new price.</summary>
    public decimal Price { get; set; }
}
