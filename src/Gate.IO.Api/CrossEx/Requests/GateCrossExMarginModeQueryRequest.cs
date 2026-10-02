namespace Gate.IO.Api.CrossEx;

/// <summary>Query the margin mode of one futures symbol; never an all-symbol query.</summary>
[JsonConverter(typeof(GateCrossExMarginModeJsonConverter))]
public record GateCrossExMarginModeQueryRequest
{
    /// <summary>Required futures trading pair. The GET contract does not restrict the exchange to Hyperliquid.</summary>
    [JsonProperty("symbol", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Symbol { get; set; }
}
