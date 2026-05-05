namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi active order
/// </summary>
public record GateTradFiOrder
{
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    [JsonProperty("price_type"), JsonConverter(typeof(MapConverter))]
    public GateTradFiOrderPriceType PriceType { get; set; }

    [JsonProperty("state")]
    public int State { get; set; }

    [JsonProperty("state_desc")]
    public string StateDescription { get; set; }

    [JsonProperty("finished")]
    public int Finished { get; set; }

    [JsonProperty("side")]
    public GateTradFiOrderSide Side { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }

    [JsonProperty("time_setup")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime SetupTime { get; set; }
}
