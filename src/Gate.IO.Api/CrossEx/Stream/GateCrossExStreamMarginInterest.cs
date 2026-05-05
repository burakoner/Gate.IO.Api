namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin interest stream update.
/// </summary>
public record GateCrossExStreamMarginInterest
{
    /// <summary>
    /// User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Interest deduction ID.
    /// </summary>
    [JsonProperty("interest_id")]
    public long? InterestId { get; set; }

    /// <summary>
    /// Liability source ID.
    /// </summary>
    [JsonProperty("liability_id")]
    public long? LiabilityId { get; set; }

    /// <summary>
    /// Liability.
    /// </summary>
    [JsonProperty("liability")]
    public decimal? Liability { get; set; }

    /// <summary>
    /// Liability coin.
    /// </summary>
    [JsonProperty("liability_coin")]
    public string LiabilityCoin { get; set; }

    /// <summary>
    /// Interest.
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Interest rate.
    /// </summary>
    [JsonProperty("interest_rate")]
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Interest deduction type.
    /// </summary>
    [JsonProperty("interest_type")]
    public string InterestType { get; set; }

    /// <summary>
    /// Create time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }
}
