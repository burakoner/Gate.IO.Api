namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Explicit increase/decrease of an existing Hyperliquid isolated futures position's margin.
/// </summary>
public record GateCrossExIsolatedMarginRequest
{
    /// <summary>
    /// Hyperliquid futures symbol, for example HYPERLIQUID_FUTURE_CXMT_USDC. Availability is server-dependent.
    /// </summary>
    [JsonProperty("symbol", Required = Required.Always)]
    public string Symbol { get; set; }

    /// <summary>
    /// Positive increases and negative decreases margin. The server truncates beyond two decimal places;
    /// the client preserves this value. Saved JSON must supply an exact decimal string or integer.
    /// </summary>
    [JsonProperty("margin", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Margin { get; set; }

    /// <summary>
    /// NONE/LONG/SHORT. Null omits the field; the server defaults to NONE for one-way positions.
    /// The client does not infer position mode or choose a hedge side.
    /// </summary>
    [JsonProperty("position_side", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExPositionSideConverter))]
    public GateCrossExPositionSide? PositionSide { get; set; }
}
