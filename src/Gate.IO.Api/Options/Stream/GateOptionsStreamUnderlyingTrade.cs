namespace Gate.IO.Api.Options;

/// <summary>
/// OptionsStreamUnderlyingTrade
/// </summary>
public record GateOptionsStreamUnderlyingTrade
{
    /// <summary>
    /// Contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// TimeInMilliseconds
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long TimeInMilliseconds { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// IsCall
    /// </summary>
    [JsonProperty("is_call")]
    public bool IsCall { get; set; }
}