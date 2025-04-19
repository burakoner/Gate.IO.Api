namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsOrderBook
/// </summary>
public record GateOptionsOrderBook
{
    /// <summary>
    /// Order Book ID. Increases by 1 on every order book change. Set with_id=true to include this field in response
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Response data generation timestamp
    /// </summary>
    [JsonProperty("current")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Current { get; set; }

    /// <summary>
    /// Order book changed timestamp
    /// </summary>
    [JsonProperty("update")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Update { get; set; }

    /// <summary>
    /// Asks order depth
    /// </summary>
    [JsonProperty("asks")]
    public List<GateOptionsOrderBookEntry> Asks { get; set; } = [];

    /// <summary>
    /// Bids order depth
    /// </summary>
    [JsonProperty("bids")]
    public List<GateOptionsOrderBookEntry> Bids { get; set; } = [];
}

/// <summary>
/// GateOptionsOrderBookEntry
/// </summary>
public record GateOptionsOrderBookEntry
{
    /// <summary>
    /// Price (quote currency)
    /// </summary>
    [JsonProperty("p")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("s")]
    public decimal Size { get; set; }
}
