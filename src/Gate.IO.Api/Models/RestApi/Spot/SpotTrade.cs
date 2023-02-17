namespace Gate.IO.Api.Models.RestApi.Spot;

/// <summary>
/// Trade
/// </summary>
public  class SpotTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Trading time
    /// </summary>
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trading time, with millisecond precision
    /// </summary>
    [JsonProperty("create_time_ms")]
    public decimal CreateTimeMilliseconds { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
    public SpotOrderSide Side { get; set; }

    /// <summary>
    /// Trade amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
}
