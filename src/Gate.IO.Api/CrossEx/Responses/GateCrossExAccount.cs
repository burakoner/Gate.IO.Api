namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account
/// </summary>
public record GateCrossExAccount
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("available_margin")]
    public decimal? AvailableMargin { get; set; }

    [JsonProperty("margin_balance")]
    public decimal? MarginBalance { get; set; }

    [JsonProperty("initial_margin")]
    public decimal? InitialMargin { get; set; }

    [JsonProperty("maintenance_margin")]
    public decimal? MaintenanceMargin { get; set; }

    [JsonProperty("initial_margin_rate")]
    public decimal? InitialMarginRate { get; set; }

    [JsonProperty("maintenance_margin_rate")]
    public decimal? MaintenanceMarginRate { get; set; }

    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    [JsonProperty("account_limit")]
    public decimal? AccountLimit { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("account_mode")]
    public string AccountMode { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("assets")]
    public List<GateCrossExAccountAsset> Assets { get; set; } = [];
}
