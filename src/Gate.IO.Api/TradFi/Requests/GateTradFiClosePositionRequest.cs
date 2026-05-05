namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi close-position request
/// </summary>
public record GateTradFiClosePositionRequest
{
    public int CloseType { get; set; }
    public decimal? CloseVolume { get; set; }
}
