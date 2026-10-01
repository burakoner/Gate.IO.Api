namespace Gate.IO.Api.CrossEx;

/// <summary>
/// HTTP 202 isolated-margin acknowledgement, not proof that the adjustment has completed.
/// </summary>
public record GateCrossExIsolatedMarginResponse
{
    /// <summary>
    /// Futures trading pair returned by the server; not filled from the request.
    /// </summary>
    [JsonProperty("symbol", Required = Required.Always)]
    public string Symbol { get; set; }

    /// <summary>
    /// Amount increased/decreased in this request, not the resulting position's total margin.
    /// </summary>
    [JsonProperty("margin", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Margin { get; set; }

    /// <summary>
    /// Optional returned NONE/LONG/SHORT side. Omitted or unfamiliar values do not identify a confirmed position side.
    /// </summary>
    [JsonProperty("position_side")]
    public string PositionSide { get; set; }
}
