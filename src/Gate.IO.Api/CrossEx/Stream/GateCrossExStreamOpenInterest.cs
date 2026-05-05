namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx futures open interest stream update.
/// </summary>
public record GateCrossExStreamOpenInterest
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Open interest.
    /// </summary>
    [JsonProperty("oi")]
    public decimal OpenInterest { get; set; }

    /// <summary>
    /// Open interest value.
    /// </summary>
    [JsonProperty("oiV")]
    public decimal? OpenInterestValue { get; set; }
}
