namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi close-position request
/// </summary>
public record GateTradFiClosePositionRequest
{
    /// <summary>
    /// Gets or sets the Close Type.
    /// </summary>
    public int CloseType { get; set; }
    /// <summary>
    /// Gets or sets the Close Volume.
    /// </summary>
    public decimal? CloseVolume { get; set; }
}
