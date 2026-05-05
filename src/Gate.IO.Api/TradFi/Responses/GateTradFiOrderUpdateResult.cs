namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order update result
/// </summary>
public record GateTradFiOrderUpdateResult
{
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("state")]
    public int State { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }
}
