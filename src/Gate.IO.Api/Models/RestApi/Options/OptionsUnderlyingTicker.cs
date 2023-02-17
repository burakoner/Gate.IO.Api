namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsUnderlyingTicker
{

    /// <summary>
    /// Total put options trades amount in last 24h
    /// </summary>
    [JsonProperty("trade_put")]
    public long TradePut { get; set; }

    /// <summary>
    /// Total call options trades amount in last 24h
    /// </summary>
    [JsonProperty("trade_call")]
    public long TradeCall { get; set; }

    /// <summary>
    /// Index price (quote currency)
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }
}