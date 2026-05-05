namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn history record
/// </summary>
public record GateEarnFixedTermHistoryRecord
{
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Unique time identifier
    /// </summary>
    [JsonProperty("uniq_time")]
    public string UniqueTime { get; set; }

    /// <summary>
    /// Reward campaign ID
    /// </summary>
    [JsonProperty("bonus_id")]
    public long? BonusId { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("product_id")]
    public long ProductId { get; set; }

    /// <summary>
    /// Reward currency
    /// </summary>
    [JsonProperty("bonus_asset")]
    public string BonusAsset { get; set; }

    /// <summary>
    /// Total principal
    /// </summary>
    [JsonProperty("total_principal")]
    public decimal TotalPrincipal { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency price
    /// </summary>
    [JsonProperty("asset_price")]
    public decimal AssetPrice { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Detail description
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("create_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateAt { get; set; }

    /// <summary>
    /// Term
    /// </summary>
    [JsonProperty("lock_up_period")]
    public int LockUpPeriod { get; set; }
}
