namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx mark price stream update.
/// </summary>
public record GateCrossExStreamMarkPrice
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Mark price.
    /// </summary>
    [JsonProperty("mp")]
    public decimal MarkPrice { get; set; }
}
