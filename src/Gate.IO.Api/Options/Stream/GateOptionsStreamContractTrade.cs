namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Contract Trade.
/// </summary>
public record GateOptionsStreamContractTrade
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Time In Milliseconds.
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long TimeInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

}
