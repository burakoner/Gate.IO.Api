namespace Gate.IO.Api.Spot;

/// <summary>
/// Ticker
/// </summary>
public class GateSpotTicker
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Last trading price
    /// </summary>
    [JsonProperty("last")]
    public decimal Last { get; set; }

    /// <summary>
    /// Recent lowest ask
    /// </summary>
    [JsonProperty("lowest_ask")]
    public decimal? LowestAsk { get; set; }

    /// <summary>
    /// Recent highest bid
    /// </summary>
    [JsonProperty("highest_bid")]
    public decimal? HighestBid { get; set; }

    /// <summary>
    /// Change percentage in the last 24h
    /// </summary>
    [JsonProperty("change_percentage")]
    public decimal ChangePercentage { get; set; }

    /// <summary>
    /// utc0 timezone, the percentage change in the last 24 hours
    /// </summary>
    [JsonProperty("change_utc0")]
    public decimal ChangeUtc0 { get; set; }

    /// <summary>
    /// utc8 timezone, the percentage change in the last 24 hours
    /// </summary>
    [JsonProperty("change_utc8")]
    public decimal ChangeUtc8 { get; set; }

    /// <summary>
    /// Base currency trade volume in the last 24h
    /// </summary>
    [JsonProperty("base_volume")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// Quote currency trade volume in the last 24h
    /// </summary>
    [JsonProperty("quote_volume")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Highest price in 24h
    /// </summary>
    [JsonProperty("high_24h")]
    public decimal High24h { get; set; }

    /// <summary>
    /// Lowest price in 24h
    /// </summary>
    [JsonProperty("low_24h")]
    public decimal Low24h { get; set; }

    /// <summary>
    /// ETF net value
    /// </summary>
    [JsonProperty("etf_net_value")]
    public decimal? EtfNetValue { get; set; }

    /// <summary>
    /// ETF previous net value at re-balancing time
    /// </summary>
    [JsonProperty("etf_pre_net_value")]
    public decimal? EtfPreviousNetValue { get; set; }

    /// <summary>
    /// ETF previous re-balancing time
    /// </summary>
    [JsonProperty("etf_pre_timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? EtfPreviousTimestamp { get; set; }

    /// <summary>
    /// ETF current leverage
    /// </summary>
    [JsonProperty("etf_leverage")]
    public decimal? EtfLeverage { get; set; }
}
