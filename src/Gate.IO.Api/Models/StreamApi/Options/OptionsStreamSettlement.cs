namespace Gate.IO.Api.Models.StreamApi.Options;

public record OptionsStreamSettlement
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("orderbook_id")]
    public long OrderBookId { get; set; }

    [JsonProperty("position_size")]
    public long PositionSize { get; set; }

    [JsonProperty("profit")]
    public decimal Profit { get; set; }

    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    [JsonProperty("tag")]
    public GateOptionsContractPeriod Period { get; set; }

    [JsonProperty("trade_id")]
    public long TradeId { get; set; }
    
    [JsonProperty("trade_size")]
    public long TradeSize { get; set; }
    
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}