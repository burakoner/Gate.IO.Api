namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock position close request
/// </summary>
public record GateStockClosePositionRequest
{
    /// <summary>Gets or sets the symbol.</summary>
    public string Symbol { get; set; }
    /// <summary>Gets or sets the close volume for a partial close.</summary>
    public decimal? CloseVolume { get; set; }
    /// <summary>Gets or sets the close type.</summary>
    public GateStockPositionCloseType CloseType { get; set; }
}
