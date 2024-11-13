namespace Gate.IO.Api.Models.StreamApi.Options;

public class OptionsStreamUserSettlement
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("realised_pnl")]
    public decimal RealisedPnl { get; set; }

    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    [JsonProperty("settle_profit")]
    public decimal SettlementProfit { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("user")]
    public long UserId { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeMilliseconds { get; set; }
}