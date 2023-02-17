namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesUserLiquidate: FuturesLiquidate
{
    /// <summary>
    /// Position leverage. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    /// <summary>
    /// Position margin. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Average entry price. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    /// <summary>
    /// Liquidation price. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("liq_price")]
    public decimal LiqPrice { get; set; }

    /// <summary>
    /// Mark price. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Liquidation order ID. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
}