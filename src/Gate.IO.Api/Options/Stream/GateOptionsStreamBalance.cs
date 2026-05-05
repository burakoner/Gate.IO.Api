namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Balance.
/// </summary>
public record GateOptionsStreamBalance
{
    /// <summary>
    /// Gets or sets the Balance.
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Gets or sets the Change.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Gets or sets the Comment.
    /// </summary>
    [JsonProperty("text")]
    public string Comment { get; set; }

    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("type")]
    public GateOptionsBalanceChangeType Type { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Time In Milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}
