namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures Stream Trade.
/// </summary>
public  class GateFuturesStreamTrade
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Create Time In Milliseconds.
    /// </summary>
    [JsonProperty("create_time_ms"), JsonConverter(typeof(GateLongConverter))]
    public long CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    [JsonProperty("size")]
    public decimal Size { get; set; }
    
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Whether the trade was internal, such as insurance-fund or ADL takeover activity.
    /// </summary>
    [JsonProperty("is_internal")]
    public bool? IsInternal { get; set; }
}
