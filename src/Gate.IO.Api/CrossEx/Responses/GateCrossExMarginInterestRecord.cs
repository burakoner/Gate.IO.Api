namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin interest deduction record
/// </summary>
public record GateCrossExMarginInterestRecord
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Interest ID.
    /// </summary>
    [JsonProperty("interest_id")]
    public long? InterestId { get; set; }

    /// <summary>
    /// Gets or sets the Liability ID.
    /// </summary>
    [JsonProperty("liability_id")]
    public long? LiabilityId { get; set; }

    /// <summary>
    /// Gets or sets the Liability.
    /// </summary>
    [JsonProperty("liability")]
    public decimal? Liability { get; set; }

    /// <summary>
    /// Gets or sets the Liability Coin.
    /// </summary>
    [JsonProperty("liability_coin")]
    public string LiabilityCoin { get; set; }

    /// <summary>
    /// Gets or sets the Interest.
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Gets or sets the Interest Rate.
    /// </summary>
    [JsonProperty("interest_rate")]
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Gets or sets the Interest Type.
    /// </summary>
    [JsonProperty("interest_type")]
    public string InterestType { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }
}
