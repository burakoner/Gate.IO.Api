namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Cross Margin Balance.
/// </summary>
public  class GateSpotStreamCrossMarginBalance
{
    /// <summary>
    /// Gets or sets the Timestamp.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

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
    /// Gets or sets the Total.
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the Available.
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Gets or sets the frozen balance.
    /// </summary>
    [JsonProperty("freeze")]
    public decimal Freeze { get; set; }

    /// <summary>
    /// Gets or sets the frozen balance change.
    /// </summary>
    [JsonProperty("freeze_change")]
    public decimal FreezeChange { get; set; }

    /// <summary>
    /// Gets or sets the balance change type.
    /// </summary>
    [JsonProperty("change_type"), JsonConverter(typeof(MapConverter))]
    public GateSpotBalanceChangeType ChangeType { get; set; }
}
