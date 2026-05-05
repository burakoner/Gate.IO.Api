namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi account assets
/// </summary>
public record GateTradFiAccountAssets
{
    [JsonProperty("equity")]
    public decimal Equity { get; set; }

    [JsonProperty("margin_level")]
    public decimal MarginLevel { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    [JsonProperty("margin_free")]
    public decimal MarginFree { get; set; }

    [JsonProperty("unrealized_pnl")]
    public decimal UnrealizedPnl { get; set; }

    [JsonProperty("mt5_uid")]
    public long Mt5Uid { get; set; }
}
