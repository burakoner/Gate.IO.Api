namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account
/// </summary>
public record GateCrossExAccount
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Available Margin.
    /// </summary>
    [JsonProperty("available_margin")]
    public decimal? AvailableMargin { get; set; }

    /// <summary>
    /// Gets or sets the Margin Balance.
    /// </summary>
    [JsonProperty("margin_balance")]
    public decimal? MarginBalance { get; set; }

    /// <summary>
    /// Gets or sets the Initial Margin.
    /// </summary>
    [JsonProperty("initial_margin")]
    public decimal? InitialMargin { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance Margin.
    /// </summary>
    [JsonProperty("maintenance_margin")]
    public decimal? MaintenanceMargin { get; set; }

    /// <summary>
    /// Gets or sets the Initial Margin Rate.
    /// </summary>
    [JsonProperty("initial_margin_rate")]
    public decimal? InitialMarginRate { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance Margin Rate.
    /// </summary>
    [JsonProperty("maintenance_margin_rate")]
    public decimal? MaintenanceMarginRate { get; set; }

    /// <summary>
    /// Gets or sets the Position Mode.
    /// </summary>
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    /// <summary>
    /// Gets or sets the Account Limit.
    /// </summary>
    [JsonProperty("account_limit")]
    public decimal? AccountLimit { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Update Time.
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Gets or sets the Account Mode.
    /// </summary>
    [JsonProperty("account_mode")]
    public string AccountMode { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Assets.
    /// </summary>
    [JsonProperty("assets")]
    public List<GateCrossExAccountAsset> Assets { get; set; } = [];
}
