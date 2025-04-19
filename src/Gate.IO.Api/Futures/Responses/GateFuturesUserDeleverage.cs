namespace Gate.IO.Api.Futures;

public record GateFuturesUserDeleverage
{
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    [JsonProperty("fill_price")]
    public decimal FillPrice { get; set; }

    [JsonProperty("position_size")]
    public long PositionSize { get; set; }

    [JsonProperty("trade_size")]
    public long TradeSize { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("user")]
    public long UserId { get; set; }
}