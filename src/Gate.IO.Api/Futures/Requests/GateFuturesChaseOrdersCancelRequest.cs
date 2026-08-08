namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase orders batch cancellation request
/// </summary>
public record GateFuturesChaseOrdersCancelRequest
{
    /// <summary>
    /// Optional contract name
    /// </summary>
    [JsonProperty("contract", NullValueHandling = NullValueHandling.Ignore)]
    public string Contract { get; set; }

    /// <summary>
    /// Optional position margin mode
    /// </summary>
    [JsonProperty("pos_margin_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesPositionMarginMode? PositionMarginMode { get; set; }
}
