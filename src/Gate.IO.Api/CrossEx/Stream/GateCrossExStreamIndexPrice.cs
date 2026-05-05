namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx index price stream update.
/// </summary>
public record GateCrossExStreamIndexPrice
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Index price.
    /// </summary>
    [JsonProperty("ip")]
    public decimal IndexPrice { get; set; }
}
