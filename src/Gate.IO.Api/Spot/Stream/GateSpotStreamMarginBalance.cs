namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Margin Balance.
/// </summary>
public  class GateSpotStreamMarginBalance
{
    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Time In Milliseconds.
    /// </summary>
    [JsonProperty("timestamp_ms")]
    public long TimeInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Currency.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Change.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }
    
    /// <summary>
    /// Gets or sets the Available.
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Gets or sets the Freeze.
    /// </summary>
    [JsonProperty("freeze")]
    public decimal Freeze { get; set; }

    /// <summary>
    /// Gets or sets the Borrowed.
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Gets or sets the Interest.
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}
