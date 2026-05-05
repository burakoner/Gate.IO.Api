namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Funding Balance.
/// </summary>
public  class GateSpotStreamFundingBalance
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
    /// Gets or sets the Change.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }
    
    /// <summary>
    /// Gets or sets the Freeze.
    /// </summary>
    [JsonProperty("freeze")]
    public decimal Freeze { get; set; }

    /// <summary>
    /// Gets or sets the Lent.
    /// </summary>
    [JsonProperty("lent")]
    public decimal Lent { get; set; }
}
