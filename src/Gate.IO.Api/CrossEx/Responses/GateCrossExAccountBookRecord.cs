namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account book record
/// </summary>
public record GateCrossExAccountBookRecord
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Business ID.
    /// </summary>
    [JsonProperty("business_id")]
    public long? BusinessId { get; set; }

    /// <summary>
    /// Gets or sets the Statement Type.
    /// </summary>
    [JsonProperty("statement_type")]
    public string StatementType { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Coin.
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Gets or sets the Change.
    /// </summary>
    [JsonProperty("change")]
    public decimal? Change { get; set; }

    /// <summary>
    /// Gets or sets the Balance.
    /// </summary>
    [JsonProperty("balance")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }
}
