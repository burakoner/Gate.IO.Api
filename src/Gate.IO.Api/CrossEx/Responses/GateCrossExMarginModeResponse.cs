namespace Gate.IO.Api.CrossEx;

/// <summary>Queried margin mode or HTTP 202 update acknowledgement, not proof that the change completed.</summary>
[JsonConverter(typeof(GateCrossExMarginModeJsonConverter))]
public record GateCrossExMarginModeResponse
{
    /// <summary>Required server-returned symbol. Not filled from the request or treated as a confirmed local state.</summary>
    [JsonProperty("symbol", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Symbol { get; set; }

    /// <summary>Required raw mode string, currently CROSS/ISOLATED. Unknown values remain uninterpreted, never mapped to CROSS.</summary>
    [JsonProperty("margin_mode", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string MarginMode { get; set; }
}
