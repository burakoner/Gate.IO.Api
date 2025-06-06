namespace Gate.IO.Api.Models.StreamApi.Spot;

public record SpotStreamTicker
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
}
