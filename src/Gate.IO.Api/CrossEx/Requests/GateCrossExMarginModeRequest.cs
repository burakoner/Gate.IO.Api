namespace Gate.IO.Api.CrossEx;

/// <summary>Explicit Hyperliquid futures margin-mode change. Open orders or positions prevent the change.</summary>
[JsonConverter(typeof(GateCrossExMarginModeJsonConverter))]
public record GateCrossExMarginModeRequest
{
    /// <summary>Required Hyperliquid futures trading pair, sent unchanged.</summary>
    [JsonProperty("symbol", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Symbol { get; set; }

    /// <summary>Required CROSS/ISOLATED instruction. No default is selected.</summary>
    [JsonProperty("margin_mode", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExMarginMode MarginMode { get; set; }
}
