namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx futures funding rate stream update.
/// </summary>
public record GateCrossExStreamFundingRate
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Current funding rate.
    /// </summary>
    [JsonProperty("r")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Next funding time in milliseconds.
    /// </summary>
    [JsonProperty("T")]
    public long NextFundingTime { get; set; }
}
