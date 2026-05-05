namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures price-triggered order query request
/// </summary>
public record GateFuturesPriceTriggeredOrderQueryRequest
{
    public GateSpotTriggerFilter Status { get; set; }
    public string Contract { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
